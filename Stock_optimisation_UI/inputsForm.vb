Imports System.IO
Imports System.Linq.Expressions
Imports Warehouse_Optimisation
Imports RestSharp
Imports Newtonsoft.Json
Imports System.DirectoryServices.ActiveDirectory
Imports System.Net.Http.Headers
Imports Microsoft.Data.SqlClient
Imports Microsoft.VisualBasic.Devices
Imports System.Drawing.Drawing2D
Imports System.Net.Sockets
Imports ADOX
Imports System.Security.Authentication.ExtendedProtection
Public Class inputsForm

    Dim SKUInputs As (Integer, Double)
    Dim WarehouseAndSkuInputs As List(Of Warehouse_inputs)
    Dim reorderInputs As List(Of (String, String, Reorder_inputs))
    Dim WarehouseInputs As Dictionary(Of String, Warehouse_independent_inputs)
    Dim WarehouseLocation As Dictionary(Of String, Location)
    Dim Suggested_input_chosen As Boolean = False
    Dim NetworkID As String
    Dim SKU As String
    Dim CreatingNewNetwork As Boolean
    Dim CreatingNewSku As Boolean
    Private ReadOnly googleAPIKey As String
    Private ReadOnly client As RestClient
    Private ReadOnly connectionString As String
    Public Sub New(NetworkID As String, SKU As String, NewNetwork As Boolean, NewSku As Boolean)

        InitializeComponent()

        ''''THIS SECTION NEEDS To BE FILLED OUT WHEN USED - CONTAINS PRIVATE INFORMATION
        '''"' DELETE THIS INFORMATION BEFORE PUSHING TO A PUBLIC REPO
        Me.googleAPIKey = "###"
        Dim DatabasePassword As String = "###"
        '''' END PRIVATE INFORMATION
        '''


        Me.client = New RestClient("https://maps.googleapis.com/")
        Me.connectionString = "Server=180.150.46.156;Database=prophit_FM_DEMO;User Id=Prophit;Password=" & DatabasePassword & ";Encrypt=False"


        Me.NetworkID = NetworkID
        Me.SKU = SKU
        Me.CreatingNewNetwork = NewNetwork
        Me.CreatingNewSku = NewSku

        '''Initally assiging all data that could be loaded in to Nothing
        Me.WarehouseAndSkuInputs = Nothing
        Me.reorderInputs = Nothing
        Me.WarehouseInputs = Nothing
        Me.SKUInputs = (-1, -1)



        Try
            '''This if Else statement loads in all relavent data based on what is being created
            If CreatingNewNetwork And Not CreatingNewSku Then
                Me.SKUInputs = databaseLoadSkuData(SKU)
            ElseIf Not CreatingNewNetwork And CreatingNewSku Then
                Me.reorderInputs = databaseLoadReorderInputs(NetworkID)
                Me.WarehouseInputs = databaseLoadWarehouseInputs(NetworkID)

            ElseIf Not CreatingNewNetwork And Not CreatingNewSku Then
                Me.SKUInputs = databaseLoadSkuData(SKU)
                Me.WarehouseInputs = databaseLoadWarehouseInputs(NetworkID)
                Me.reorderInputs = databaseLoadReorderInputs(NetworkID)
                Me.WarehouseAndSkuInputs = databaseLoadWarehouseAndSkuInputs(NetworkID, SKU)
            End If
        Catch exp As Exception
            MessageBox.Show(exp.Message)
            Throw New Exception("Database load failed")
        End Try

        loadWarehouseLocationsTab()
        InputsTabControl.SelectedTab = WarehouseLocationsTab

        '''Hides all tabs except the warehouse inputs Tab
        For Each Inputtab In InputsTabControl.TabPages
            If Inputtab.Name <> "WarehouseLocationsTab" Then
                InputsTabControl.TabPages.Remove(Inputtab)
            End If
        Next

    End Sub

    ''' <summary>
    ''' This function returns the inputs in a form that can be used by the main form
    ''' and can be used by the subsequent uses. 
    ''' </summary>
    ''' <returns></returns>
    Public Function return_inputs() As (List(Of Warehouse_inputs), List(Of (String, String, Reorder_inputs)), Dictionary(Of String, String))

        Dim reorderRelationForReturn = New List(Of (String, String, Reorder_inputs))
        Dim warehouseAddresses = New Dictionary(Of String, String)

        For Each reorder In Me.reorderInputs
            If reorder.Item2 <> "-1" Then
                reorderRelationForReturn.Add(reorder)
            End If
        Next

        For Each warehouseID In Me.WarehouseInputs.Keys()
            warehouseAddresses.Add(warehouseID, Me.WarehouseInputs(warehouseID).Address)
        Next

        Return (Me.WarehouseAndSkuInputs, reorderRelationForReturn, warehouseAddresses)
    End Function



    Private Function databaseLoadReorderInputs(NetworkID As String) As List(Of (String, String, Reorder_inputs))

        Dim loadedReorderRelations = New List(Of (String, String, Reorder_inputs))

        Dim query = "SELECT * FROM prophit.[OPT.Reorder_Relation] WHERE Network_ID = @NetworkID"
        Using conn As New SqlConnection(connectionString)
            Try
                conn.Open()
                Using cmd As New SqlCommand(query, conn)
                    cmd.Parameters.AddWithValue("@NetworkID", NetworkID)
                    Dim reader As SqlDataReader = cmd.ExecuteReader()
                    While reader.Read()
                        Dim warehouseID = Convert.ToString(reader("Warehouse_ID"))
                        Dim reorderFromID = Convert.ToString(reader("Warehouse_from_ID"))
                        Dim leadTimeMean = Convert.ToDouble(reader("Lead_time_mean"))
                        Dim leadTimeSD = Convert.ToDouble(reader("Lead_time_std"))
                        Dim reorderCost = Convert.ToDouble(reader("Reorder_cost"))
                        Dim reorderInput = New Reorder_inputs(leadTimeMean, leadTimeSD, reorderCost)
                        loadedReorderRelations.Add((warehouseID, reorderFromID, reorderInput))
                    End While
                End Using
            Catch exp As Exception
                Throw New Exception("Error loading reorder relationship from database:" & exp.Message)
            End Try
        End Using

        If loadedReorderRelations.Count = 0 Then
            Throw New Exception("No reorder relationships were loaded - data may not exist for " & NetworkID)
        End If

        Return loadedReorderRelations
    End Function

    Private Function databaseLoadWarehouseAndSkuInputs(NetworkID As String, SKU As String) As List(Of Warehouse_inputs)
        Dim LoadedWarehouseSkuData = New List(Of Warehouse_inputs)

        Dim query = "SELECT * FROM prophit.[OPT.WarehouseAndSKU] WHERE Network_ID = @NetworkID AND SKU = @SKU"
        Using conn As New SqlConnection(connectionString)
            Try
                conn.Open()
                Using cmd As New SqlCommand(query, conn)
                    cmd.Parameters.AddWithValue("@NetworkID", NetworkID)
                    cmd.Parameters.AddWithValue("@SKU", SKU)
                    Dim reader As SqlDataReader = cmd.ExecuteReader()
                    While reader.Read()
                        Dim warehouseID = Convert.ToString(reader("Warehouse_ID"))
                        Dim initialInventory = Convert.ToInt32(reader("Initial_inventory"))
                        Dim demandMean = Convert.ToDouble(reader("Demand_mean"))
                        Dim demandStd = Convert.ToDouble(reader("Demand_std"))
                        Dim reorderPoint = Convert.ToDouble(reader("Reorder_point"))
                        Dim reorderAmount = Convert.ToInt32(reader("Reorder_amount"))


                        '''This uses both the currently loaded data and the previously loaded data to create a new warehouse input
                        '''Ít doesn't work out if there is a corresponding matching reorder relationship.
                        Dim NewWarehouseInput = New Warehouse_inputs(warehouseID, initialInventory, demandMean, demandStd, reorderPoint,
                                                                     reorderAmount, -1, -1, Me.WarehouseInputs(warehouseID).Warehouse_SiteType,
                                                                     Me.SKUInputs.Item2, Me.WarehouseInputs(warehouseID).Holding_cost_per_pallet,
                                                                     Me.SKUInputs.Item1, -1)
                        LoadedWarehouseSkuData.Add(NewWarehouseInput)
                    End While
                End Using
            Catch exp As Exception
                Throw New Exception("Error loading Combined Warehouse and SKU Data from database:" & exp.Message)
            End Try
        End Using

        AddRelationsToWarehouses(LoadedWarehouseSkuData)

        Return LoadedWarehouseSkuData
    End Function


    ''' <summary>
    ''' This function adds the reorder relationships to the warehouses that were loaded in from the database'. 
    ''' Make sure the global variable ReorderInputs is loaded in before calling this function
    ''' </summary>
    ''' <param name="loadedWarehouses"></param>
    Private Sub AddRelationsToWarehouses(loadedWarehouseAndSKUData As List(Of Warehouse_inputs))
        For Each warehouse In loadedWarehouseAndSKUData
            If warehouse.site_type = SiteType.Base_Warehouse Then
                For Each reorder In reorderInputs
                    If reorder.Item1 = warehouse.warehouse_id Then
                        warehouse.lead_time_mean = reorder.Item3.lead_time_mean
                        warehouse.lead_time_sd = reorder.Item3.lead_time_sd
                        warehouse.reorder_cost = reorder.Item3.reorder_cost
                    End If
                Next
            End If
        Next
    End Sub

    Private Function databaseLoadWarehouseInputs(NetworkID As String) As Dictionary(Of String, Warehouse_independent_inputs)

        Dim loadedWarehouses = New Dictionary(Of String, Warehouse_independent_inputs)

        Dim query = "SELECT * FROM prophit.[OPT.Warehouse] WHERE Network_ID = @NetworkID"
        Using conn As New SqlConnection(connectionString)
            Try
                conn.Open()
                Using cmd As New SqlCommand(query, conn)
                    cmd.Parameters.AddWithValue("@NetworkID", NetworkID)
                    Dim reader As SqlDataReader = cmd.ExecuteReader()
                    While reader.Read()

                        Dim warehouseID = Convert.ToString(reader("Warehouse_ID"))
                        Dim Address = reader("Address").ToString()
                        Dim HoldingCost = Convert.ToDouble(reader("Holding_cost"))


                        Dim SiteTypeString = reader("SiteType").ToString()
                        Dim Warehouse_SiteType = If(SiteTypeString = "Dependent Warehouse", SiteType.Dependent_Warehouse, SiteType.Base_Warehouse)

                        Dim NewWarehouseInputs = New Warehouse_independent_inputs(Address, Warehouse_SiteType, HoldingCost)
                        loadedWarehouses.Add(warehouseID, NewWarehouseInputs)
                    End While
                End Using
            Catch exp As Exception
                Throw New Exception("Error loading WarehouseInputs from database:" & exp.Message)
            End Try
        End Using

        If loadedWarehouses.Count = 0 Then
            Throw New Exception("No warehouses were loaded - Data may not exist for " & NetworkID)
        End If

        Return loadedWarehouses
    End Function

    Private Function databaseLoadSkuData(SKU As String) As (Integer, Double)
        Dim LoadedSKUInputs As (Integer, Double) = (-1, -1)

        Dim query = "SELECT * FROM prophit.[OPT.Item] WHERE SKU = @SKU"
        Using conn As New SqlConnection(connectionString)
            Try
                conn.Open()
                Using cmd As New SqlCommand(query, conn)
                    cmd.Parameters.AddWithValue("@SKU", SKU)
                    Dim reader As SqlDataReader = cmd.ExecuteReader()
                    While reader.Read()

                        Dim ProfitPerItem = Convert.ToDouble(reader("Gross_profit"))
                        Dim Items_per_palley = Convert.ToInt32(reader("Items_per_pallet"))
                        LoadedSKUInputs = (Items_per_palley, ProfitPerItem)
                    End While
                End Using
            Catch exp As Exception
                Throw New Exception("Error loading SKU data from database:" & exp.Message)
            End Try
        End Using

        If LoadedSKUInputs.Item1 = -1 And LoadedSKUInputs.Item2 = -1 Then
            Throw New Exception("Sku INputs were not loaded - Data may not exist for " & SKU)
        End If

        Return LoadedSKUInputs
    End Function


    ''' <summary>
    ''' This takes the global warehouseInputs and update the database. It does an UPSERT and 
    ''' updates the database or inserts depends if the warehouse already exists. It then Deletes warehouses
    ''' That have been removed from the network
    ''' </summary>
    Private Sub UpdateDatabaseWarehouseInputs()
        Dim WarehouseIDs = Me.WarehouseInputs.Select(Function(w) w.Key).ToList()
        Dim WarehouseIDListString As String = String.Join(",", WarehouseIDs.Select(Function(id) "'" & id & "'"))

        Dim query As String = "
            MERGE INTO prophit.[OPT.Warehouse] AS Target
            USING (VALUES (@Network_ID, @Warehouse_ID, @Address, @SiteType, @Holding_cost)) AS source (Network_ID, Warehouse_ID, Address, SiteType, Holding_cost)
            ON Target.Network_ID = source.Network_ID AND Target.Warehouse_ID = source.Warehouse_ID
            WHEN MATCHED THEN
                UPDATE SET Address = source.Address, SiteType = source.SiteType, Holding_cost = source.Holding_cost
            WHEN NOT MATCHED THEN
                INSERT (Network_ID, Warehouse_ID, Address, SiteType, Holding_cost)
                VALUES (source.Network_ID, source.Warehouse_ID, source.Address, source.SiteType, source.Holding_cost);
            DELETE FROM prophit.[OPT.Warehouse]
            WHERE Network_ID = @Network_ID AND Warehouse_ID NOT IN (" & WarehouseIDListString & ");"


        Using conn As New SqlConnection(Me.connectionString)
            Try
                conn.Open()
                For Each warehouseID In Me.WarehouseInputs.Keys()
                    Dim warehouse = WarehouseInputs(warehouseID)


                    Using cmd As New SqlCommand(query, conn)
                        cmd.Parameters.AddWithValue("@Network_ID", Me.NetworkID)
                        cmd.Parameters.AddWithValue("@Warehouse_ID", warehouseID)
                        cmd.Parameters.AddWithValue("@Address", warehouse.Address)
                        cmd.Parameters.AddWithValue("@SiteType", If(warehouse.Warehouse_SiteType = SiteType.Base_Warehouse, "Base Warehouse", "Dependent Warehouse"))
                        cmd.Parameters.AddWithValue("@Holding_cost", warehouse.Holding_cost_per_pallet)
                        cmd.ExecuteNonQuery()
                    End Using
                Next
            Catch ex As Exception
                MessageBox.Show("Error updating Warehouse inputs to database: " & ex.Message)

            End Try
        End Using

    End Sub


    ''' <summary>
    ''' This function deletes all the order reorder relations and adds the new ones back in. This was simpler
    ''' Than working out which records are still valid as it requires compariosns of tuples. Given the small amount of records
    ''' This doesn not overly impact performance.
    ''' </summary>
    Private Sub updateDatabaseReorderRelationship()

        Dim queryDelete As String = "
        DELETE FROM prophit.[OPT.Reorder_Relation]
        WHERE Network_ID = @Network_ID;"

        Dim queryInsert As String = "
        INSERT INTO prophit.[OPT.Reorder_Relation] (Network_ID, Warehouse_ID, Warehouse_from_ID, Lead_time_mean, Lead_time_std, Reorder_cost)
        VALUES (@Network_ID, @Warehouse_ID, @Warehouse_from_ID, @Lead_time_mean, @Lead_time_std, @Reorder_cost);"

        Using conn As New SqlConnection(Me.connectionString)
            Try
                conn.Open()

                ' Step 1: Delete all reorder relations for this Network_ID
                Using cmdDelete As New SqlCommand(queryDelete, conn)
                    cmdDelete.Parameters.AddWithValue("@Network_ID", Me.NetworkID)
                    cmdDelete.ExecuteNonQuery()
                End Using

                ' Step 2: Insert valid reorder relations from reorderInputs
                For Each relation In Me.reorderInputs
                    Dim warehouseID = relation.Item1
                    Dim reorderFromID = relation.Item2
                    Dim reorderInfo = relation.Item3

                    Using cmdInsert As New SqlCommand(queryInsert, conn)
                        cmdInsert.Parameters.AddWithValue("@Network_ID", Me.NetworkID)
                        cmdInsert.Parameters.AddWithValue("@Warehouse_ID", warehouseID)
                        cmdInsert.Parameters.AddWithValue("@Warehouse_from_ID", reorderFromID)
                        cmdInsert.Parameters.AddWithValue("@Lead_time_mean", reorderInfo.lead_time_mean)
                        cmdInsert.Parameters.AddWithValue("@Lead_time_std", reorderInfo.lead_time_sd)
                        cmdInsert.Parameters.AddWithValue("@Reorder_cost", reorderInfo.reorder_cost)
                        cmdInsert.ExecuteNonQuery()
                    End Using
                Next

            Catch ex As Exception
                MessageBox.Show("Error updating Reorder Relation inputs to database: " & ex.Message)
            End Try
        End Using
    End Sub






    ''' <summary>
    ''' This function updates the database with the warehouse and SKU inputs. It does an UPSERT
    ''' and updates the database or inserts depending if the warehouse and SKU inputs already exist
    ''' make sure the global variable WarehouseAndSkuInputs is loaded in before calling this function
    ''' It also deletes warehouse and SKU infromation for warehouses that have been deleted from the network
    ''' </summary>
    Private Sub updateDatabaseWarehouseAndSKUInputs()
        Dim WarehouseIDs = Me.WarehouseAndSkuInputs.Select(Function(w) w.warehouse_id).ToList()
        Dim WarehouseIDListString As String = String.Join(",", WarehouseIDs.Select(Function(id) "'" & id & "'"))

        Dim query As String = "
            MERGE INTO prophit.[OPT.WarehouseAndSKU] AS Target
            USING (VALUES (@Network_ID, @SKU, @Warehouse_ID, @Initial_inventory, @Demand_mean, @Demand_std, @Reorder_point, @Reorder_amount)) AS source (Network_ID, SKU, Warehouse_ID, Initial_inventory, Demand_mean, Demand_std, Reorder_point, Reorder_amount)
            ON Target.Network_ID = source.Network_ID AND Target.SKU = source.SKU AND Target.Warehouse_ID = source.Warehouse_ID
            WHEN MATCHED THEN
                UPDATE SET Initial_inventory = source.Initial_inventory, Demand_mean = source.Demand_mean, Demand_std = source.Demand_std, Reorder_point = source.Reorder_point, Reorder_amount = source.Reorder_amount
            WHEN NOT MATCHED THEN
                INSERT (Network_ID, SKU, Warehouse_ID, Initial_inventory, Demand_mean, Demand_std, Reorder_point, Reorder_amount)
                VALUES (source.Network_ID, source.SKU, source.Warehouse_ID, source.Initial_inventory, source.Demand_mean, source.Demand_std, source.Reorder_point, source.Reorder_amount);
            DELETE From prophit.[OPT.WarehouseAndSKU]
            WHERE Network_ID = @Network_ID And SKU = @SKU And Warehouse_ID Not IN (" & WarehouseIDListString & ");"


        Using conn As New SqlConnection(Me.connectionString)
            Try
                conn.Open()
                For Each warehouseInput In Me.WarehouseAndSkuInputs
                    Using cmd As New SqlCommand(query, conn)
                        cmd.Parameters.AddWithValue("@Network_ID", Me.NetworkID)
                        cmd.Parameters.AddWithValue("@SKU", Me.SKU)
                        cmd.Parameters.AddWithValue("@Warehouse_ID", warehouseInput.warehouse_id)
                        cmd.Parameters.AddWithValue("@Initial_inventory", warehouseInput.initial_inventory)
                        cmd.Parameters.AddWithValue("@Demand_mean", warehouseInput.demand_mean)
                        cmd.Parameters.AddWithValue("@Demand_std", warehouseInput.demand_sd)
                        cmd.Parameters.AddWithValue("@Reorder_point", warehouseInput.reorder_point)
                        cmd.Parameters.AddWithValue("@Reorder_amount", warehouseInput.reorder_amount)
                        cmd.ExecuteNonQuery()
                    End Using
                Next
            Catch ex As Exception
                MessageBox.Show("Error updating Warehouse and SKU inputs to database: " & ex.Message)
            End Try
        End Using
    End Sub

    Private Sub loadItemInputsTab()

        If Me.SKUInputs.Item1 <> -1 And Me.SKUInputs.Item2 <> -1 Then
            txtboxItemsPerPallet.Text = SKUInputs.Item1.ToString()
            txtboxProfitPerSale.Text = SKUInputs.Item2.ToString()
        End If
    End Sub


    '''
    '''<summary>
    '''This function does an UPSERT meaning it updates the database if the warehouse already exists
    '''Or inserts the SKU data if it is a new input
    '''</summary>
    '''<returns></returns>
    Private Sub UpdateDatabaseSKUInputs()

        Dim query As String = "
            MERGE INTO prophit.[OPT.Item] AS Target
            USING (VALUES (@SKU, @Gross_profit, @Items_per_pallet)) AS source (SKU, Gross_profit, Items_per_pallet)
            ON Target.SKU = source.SKU
            WHEN MATCHED THEN
                UPDATE SET Gross_profit = source.Gross_profit, Items_per_pallet = source.Items_per_pallet
            WHEN NOT MATCHED THEN
                INSERT (SKU, Gross_profit, Items_per_pallet)
                VALUES (source.SKU, source.Gross_profit, source.Items_per_pallet);"




        Using conn As New SqlConnection(Me.connectionString)
            Try
                conn.Open()

                Using cmd As New SqlCommand(query, conn)
                    cmd.Parameters.AddWithValue("@Network_ID", Me.NetworkID)
                    cmd.Parameters.AddWithValue("@SKU", Me.SKU)
                    cmd.Parameters.AddWithValue("@Gross_profit", Me.SKUInputs.Item2)
                    cmd.Parameters.AddWithValue("@Items_per_pallet", Me.SKUInputs.Item1)
                    cmd.ExecuteNonQuery()
                End Using
            Catch ex As Exception
                MessageBox.Show("Error updating SKU inputs to database: " & ex.Message)
            End Try
        End Using
    End Sub



    Private Sub btnSubmitItemInputs_Click(Sender As Object, e As EventArgs) Handles btnSubmitItemInputs.Click
        Try

            Dim items_per_pallet As Integer = Convert.ToInt32(txtboxItemsPerPallet.Text)
            Dim profit_per_item As Double = Convert.ToDouble(txtboxProfitPerSale.Text)

            Me.SKUInputs = (items_per_pallet, profit_per_item)

            WarehouseAndSKUInputsLoadtab()
            UpdateDatabaseSKUInputs()
            InputsTabControl.TabPages.Add(WarehouseAndSKUInputsTab)
            InputsTabControl.TabPages.Remove(ItemInputsTab)
            InputsTabControl.SelectedTab = WarehouseAndSKUInputsTab


        Catch formatEx As FormatException
            MessageBox.Show("Please enter an integer value for the items per pallet, and a valid number for the profit per item")

        Catch ex As OverflowException
            ' Handle overflow errors (too large or small numbers)
            MessageBox.Show("The number you entered is out of range. Please enter smaller numeric values.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error)

        Catch ex As Exception
            MessageBox.Show("An unknown error occuered: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub WarehouseAndSKUInputsLoadtab()

        '''This adds data that was loaded in from the database
        If WarehouseAndSkuInputs IsNot Nothing Then
            For Each warehouseInput In WarehouseAndSkuInputs
                If WarehouseStillExists(warehouseInput.warehouse_id) And Not WarehouseInTable(warehouseInput.warehouse_id, DataGridWarehouse) Then
                    Dim newRow As DataGridViewRow = New DataGridViewRow()
                    newRow.CreateCells(DataGridWarehouse)

                    newRow.Cells(0).Value = warehouseInput.warehouse_id
                    newRow.Cells(1).Value = warehouseInput.initial_inventory
                    newRow.Cells(2).Value = warehouseInput.demand_mean
                    newRow.Cells(3).Value = warehouseInput.demand_sd
                    newRow.Cells(4).Value = warehouseInput.reorder_point
                    newRow.Cells(5).Value = warehouseInput.reorder_amount
                    newRow.Cells(7).Value = Me.WarehouseInputs(warehouseInput.warehouse_id).Holding_cost_per_pallet
                    If WarehouseInputs(warehouseInput.warehouse_id).Warehouse_SiteType = SiteType.Base_Warehouse Then
                        newRow.Cells(6).Value = "Base Warehouse"
                    ElseIf WarehouseInputs(warehouseInput.warehouse_id).Warehouse_SiteType = SiteType.Dependent_Warehouse Then
                        newRow.Cells(6).Value = "Dependent Warehouse"
                    End If
                    DataGridWarehouse.Rows.Add(newRow)
                End If
            Next
        End If



        '''this part then adds any other warehouses to the table, or adds them all
        '''if the database was not loaded in 
        For Each warehouseID In WarehouseInputs.Keys()
            If Not WarehouseInTable(warehouseID, DataGridWarehouse) Then
                Dim newRow As DataGridViewRow = New DataGridViewRow()


                newRow.CreateCells(DataGridWarehouse)
                newRow.Cells(0).Value = warehouseID
                newRow.Cells(7).Value = WarehouseInputs(warehouseID).Holding_cost_per_pallet


                If WarehouseInputs(warehouseID).Warehouse_SiteType = SiteType.Base_Warehouse Then

                    newRow.Cells(6).Value = "Base Warehouse"

                ElseIf WarehouseInputs(warehouseID).Warehouse_SiteType = SiteType.Dependent_Warehouse Then
                    newRow.Cells(6).Value = "Dependent Warehouse"
                End If

                DataGridWarehouse.Rows.Add(newRow)
            End If

        Next

    End Sub

    ''Checks if warehouse is already in a datagrid view - assumes the WarehouseID
    ''Is in the first column
    Private Function WarehouseInTable(WarehouseID As String, table As DataGridView)
        For Each row In table.Rows
            If Not row.IsNewRow AndAlso row.Cells(0).Value IsNot Nothing Then
                If row.Cells(0).Value = WarehouseID Then
                    Return True
                End If
            End If
        Next
        Return False
    End Function

    ''Checks if a reorder is already in the reorders Table - assues the WarehouseID and the 
    '' Reorders From ID are the first two columns of the table. 
    Private Function ReorderInTable(WarehouseID As String, ReorderWarehouseID As String, table As DataGridView)
        For Each row In table.Rows
            If Not row.IsNewRow AndAlso row.Cells(0).Value IsNot Nothing Then
                If row.Cells(0).Value = WarehouseID And row.Cells(1).value = ReorderWarehouseID Then
                    Return True
                End If
            End If
        Next
        Return False
    End Function

    ''' <summary>
    ''' Checkds if the warehouse loaded in from the database has not been deleted during the
    ''' warehouse inputs stage
    ''' </summary>
    ''' <returns></returns>
    Private Function WarehouseStillExists(WarehouseID As String) As Boolean
        For Each WarehouseIDKey In Me.WarehouseInputs.Keys()
            If WarehouseIDKey = WarehouseID Then
                Return True
            End If
        Next
        Return False

    End Function


    Private Sub btnSubmitWarehouseSkuData_Click(Sender As Object, e As EventArgs) Handles btnNextWarehouse.Click

        If validateWarehouseInputs() Then
            AddRelationsToWarehouses(Me.WarehouseAndSkuInputs)
            updateDatabaseWarehouseAndSKUInputs()
            LoadShowWarehousetab()


            InputsTabControl.TabPages.Add(ShowWarehouseTabPage)
            InputsTabControl.TabPages.Remove(WarehouseAndSKUInputsTab)
            InputsTabControl.SelectedTab = ShowWarehouseTabPage

        Else
            MessageBox.Show("Current Warehouse inputs given are invalid")
        End If
    End Sub



    Private Sub LoadShowWarehousetab()
        retriveWarehouseLocations()


        createHTMLFile()

        MapsForm_Load()


    End Sub


    Private Sub createHTMLFile()


        Dim firstHalfFile = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "MapsFirstHalf.html")
        Dim secondHalfFile = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "MapsSecondHalf.html")
        Dim outputFile = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Maps.html")

        Dim firstHalfHTMLString As String = File.ReadAllText(firstHalfFile)
        Dim secondHalfHTMLString As String = File.ReadAllText(secondHalfFile)



        Dim mapString = "            var map = new google.maps.Map(document.getElementById('map'), {
                center: { lat: " & Me.WarehouseLocation(WarehouseLocation.Keys()(0)).latitude & ", lng: " & Me.WarehouseLocation(WarehouseLocation.Keys()(0)).longitude & " },
                zoom: 4,
            });
"


        Dim markersString = ""

        For Each warehouse In Me.WarehouseLocation
            Dim lat = warehouse.Value.latitude
            Dim lng = warehouse.Value.longitude
            Dim nextmarkerString = "            var marker" & warehouse.Key & " = new google.maps.Marker({
                map,
                position:   {lat: " & lat & ", lng: " & lng & "},
            });
                marker" & warehouse.Key & ".setMap(map);

"
            markersString = markersString + nextmarkerString
        Next


        Dim reordersString = ""

        For Each reorder In reorderInputs
            Dim warehouse1ID = reorder.Item1
            Dim warehouse2ID = reorder.Item2
            If warehouse2ID <> "-1" Then

                Dim nextreorderString = "            const reorderPoints" & warehouse1ID & "_" & warehouse2ID & " = [
                { lat: " & WarehouseLocation(warehouse1ID).latitude & ", lng: " & WarehouseLocation(warehouse1ID).longitude & " },
                { lat: " & WarehouseLocation(warehouse2ID).latitude & ", lng: " & WarehouseLocation(warehouse2ID).longitude & " },
            ];

            const reorderPath" & warehouse1ID & "_" & warehouse2ID & " = new google.maps.Polyline({
                path: reorderPoints" & warehouse1ID & "_" & warehouse2ID & ",
                geodesic: false,
                strokeColor: ""#FF0000"",
                strokeOpacity: 1.0,
                strokeWeight: 2,

            });
            reorderPath" & warehouse1ID & "_" & warehouse2ID & ".setMap(map);
    
"

                reordersString = reordersString + nextreorderString
            End If
        Next


        Dim totalHTMLString = firstHalfHTMLString & mapString & markersString & reordersString & secondHalfHTMLString

        File.WriteAllText(outputFile, totalHTMLString)

    End Sub

    Private Async Sub MapsForm_Load()

        Await MapsWebView.EnsureCoreWebView2Async(Nothing)
        MapsWebView.CoreWebView2.Settings.IsScriptEnabled = True
        MapsWebView.CoreWebView2.Settings.AreDevToolsEnabled = True


        Dim htmlFilePath As String = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Maps.html")

        If File.Exists(htmlFilePath) Then
            MapsWebView.Source = New Uri(htmlFilePath)
        Else
            MessageBox.Show(htmlFilePath & " not found")
        End If

    End Sub


    ''' <summary>
    ''' This funcation takes in the addresses of warehouses and and the geocoding google maps
    ''' API to create a location - which includes a lat and long. This then adds the location 
    ''' To the global warehouse variable
    ''' </summary>
    Private Sub retriveWarehouseLocations()

        Dim warehouseLocations = New Dictionary(Of String, Location)

        For Each warehouseID In WarehouseInputs.Keys()
            Dim APIResponce As MyResults = Nothing
            Dim address As String = Nothing
            Try
                address = WarehouseInputs(warehouseID).Address
                Dim request = New RestRequest($"maps/api/geocode/json?address=" & address & "&key=" & Me.googleAPIKey, Method.Get)
                Dim response = Me.client.Execute(request).Content

                If response IsNot Nothing Then
                    APIResponce = JsonConvert.DeserializeObject(Of MyResults)(response)
                End If

            Catch ex As Exception
                MessageBox.Show(ex.Message)
            End Try


            For Each result In APIResponce.results

                warehouseLocations.Add(warehouseID, New Location(address, result.geometry.location.lat, result.geometry.location.lng))

            Next
        Next
        Me.WarehouseLocation = warehouseLocations

        ''pop up used for debugging, telling you the lng and lat found
        'Dim resultsString = ""
        'For Each warehouseID In Me.WarehouseLocation.Keys()
        '    resultsString = resultsString & "Warehouse " & warehouseID & " lat - " & Me.WarehouseLocation(warehouseID).latitude & " lng - " & Me.WarehouseLocation(warehouseID).longitude & vbCrLf
        'Next
        'MessageBox.Show(resultsString)

    End Sub




    Private Sub loadReorderInputstab()

        DataGridReorders.SelectionMode = DataGridViewSelectionMode.FullRowSelect

        If reorderInputs IsNot Nothing Then
            For Each reorder In reorderInputs
                Dim newRow As DataGridViewRow = New DataGridViewRow()
                newRow.CreateCells(DataGridReorders)
                If WarehouseStillExists(reorder.Item1) And (WarehouseStillExists(reorder.Item2) Or reorder.Item2 = "-1") Then
                    If Not ReorderInTable(reorder.Item1, reorder.Item2, DataGridReorders) Then
                        newRow.Cells(0).Value = reorder.Item1
                        newRow.Cells(1).Value = reorder.Item2
                        newRow.Cells(2).Value = reorder.Item3.lead_time_mean
                        newRow.Cells(3).Value = reorder.Item3.lead_time_sd
                        newRow.Cells(4).Value = reorder.Item3.reorder_cost
                        DataGridReorders.Rows.Add(newRow)
                    End If
                End If
            Next
        End If

        For Each warehouseID In Me.WarehouseInputs.Keys()
            If Me.WarehouseInputs(warehouseID).Warehouse_SiteType = SiteType.Base_Warehouse Then
                If Not WarehouseInTable(warehouseID, DataGridReorders) Then
                    Dim newReorderRow As DataGridViewRow = New DataGridViewRow()
                    newReorderRow.CreateCells(DataGridReorders)
                    newReorderRow.Cells(0).Value = warehouseID
                    newReorderRow.Cells(1).Value = -1
                    DataGridReorders.Rows.Add(newReorderRow)
                End If
            End If

        Next


    End Sub

    Private Sub btnSubmitInputs_Click(Sender As Object, e As EventArgs) Handles btnSubmitReorderInputs.Click
        If validateReorderInputs() Then

            updateDatabaseReorderRelationship()

            loadItemInputsTab()
            InputsTabControl.TabPages.Add(ItemInputsTab)
            InputsTabControl.TabPages.Remove(WarehouseRelationsTab)
            InputsTabControl.SelectedTab = ItemInputsTab

        Else
            MessageBox.Show("Reorder relationship inputs are invalid. Ensure Warehouse IDs match with previous tab")
        End If
    End Sub


    Private Function validateWarehouseInputs() As Boolean
        Me.WarehouseAndSkuInputs = New List(Of Warehouse_inputs)
        Try
            For Each row In DataGridWarehouse.Rows
                If Not row.IsNewRow Then
                    Dim selectedSiteType As String = row.Cells("Site_type").Value.ToString()
                    Dim siteType As SiteType
                    If selectedSiteType = "Base Warehouse" Then
                        siteType = SiteType.Base_Warehouse
                    ElseIf selectedSiteType = "Dependent Warehouse" Then
                        siteType = SiteType.Dependent_Warehouse
                    Else
                        siteType = SiteType.Dependent_Warehouse ''This is useless now, however is good to have a default case if this if statement is expanded
                    End If

                    Dim warehouseInput As New Warehouse_inputs(
                        warehouse_id:=Convert.ToString(row.Cells("Warehouse_ID").Value),
                        initial_inventory:=Convert.ToInt32(row.Cells("Initial_inventory").Value),
                        demand_mean:=Convert.ToDouble(row.Cells("Demand_mean").Value),
                        demand_sd:=Convert.ToDouble(row.Cells("Demand_std_dev").Value),
                        reorder_point:=Convert.ToDouble(row.Cells("Reorder_point").Value),
                        reorder_amount:=Convert.ToInt32(row.Cells("Reorder_amount").Value),
                        lead_time_mean:=-1,
                        lead_time_sd:=-1,
                        site_type:=siteType,
                        profit_per_sale:=Me.SKUInputs.Item2,
                        holding_cost_per_pallet:=Convert.ToDouble(row.Cells("Holding_cost_per_pallet").Value),
                        items_per_pallet:=Me.SKUInputs.Item1,
                        reorder_cost:=-1
                    )
                    WarehouseAndSkuInputs.Add(warehouseInput)
                End If

            Next
            Return True

        Catch ex As Exception
            Return False

        End Try



        Return True
    End Function

    Private Function validateReorderInputs() As Boolean
        Try
            Me.reorderInputs = New List(Of (String, String, Reorder_inputs))
            For Each row In DataGridReorders.Rows
                If Not row.IsNewRow Then
                    add_reorder_relationships(row)
                End If
            Next

        Catch ex As Exception
            MessageBox.Show("Issue with the numbers given in the data")
            Return False

        End Try
        ''then checks if every warehouse has an approriate reorder location
        Return check_reorder_inputs_match()

    End Function

    ''' <summary>
    ''' This function checks if a reorder relationship already exists
    ''' if IsBase is true then checks that the reorder from field is set to -1
    ''' </summary>
    ''' <param name="warehouseID"> The Warehouse ID of the warehouse you are checking as a reorder</param>
    ''' <param name="isBase"> whether the warehouse you are checking is a base warehouse or not</param>
    ''' <returns></returns>
    Private Function reorderPartnerExists(warehouseID As String, isBase As Boolean) As Boolean
        For Each reorder In reorderInputs
            If reorder.Item1 = warehouseID Then

                '''checks that only base warehouses have the reorder from set to -1
                If isBase And reorder.Item2 <> "-1" Then
                    Return False
                ElseIf Not isBase And reorder.Item2 = "-1" Then
                    Return False
                End If

                Return True
            End If
        Next
        Return False
    End Function

    ''' <summary>
    ''' /This function checks if a base warehouse exists in the warehouse inputs
    ''' </summary>
    ''' <param name="warehouseIDToCheck"> TheID of the warehouse to check</param>
    ''' <returns></returns>
    Private Function checkBaseWarehouseExists(warehouseIDToCheck As String) As Boolean
        For Each warehouseID In Me.WarehouseInputs.Keys()
            If Me.WarehouseInputs(warehouseID).Warehouse_SiteType = SiteType.Base_Warehouse And warehouseIDToCheck = warehouseID Then
                Return True
            End If
        Next
        Return False
    End Function

    ''' <summary>
    ''' This function checks if both warehouses in a reorder relationship exist ALso checks the reorder is not from a 
    ''' base warehouse
    ''' </summary>
    ''' <param name="warehouseIDToCheck"> The ID of the warehouse to check</param>
    ''' <param name="reorderFromWarehouseID"> The ID of the warehouse that the warehouse to check reorders from</param>
    ''' <returns></returns>
    Private Function checkWarehousesExist(warehouseIDToCheck As String, reorderFromWarehouseID As String) As Boolean
        Dim warehouse1Exists As Boolean = False
        Dim warehouse2Exists As Boolean = False

        For Each warehouseID In Me.WarehouseInputs.Keys()
            If warehouseID = warehouseIDToCheck And WarehouseInputs(warehouseID).Warehouse_SiteType <> SiteType.Base_Warehouse Then
                warehouse1Exists = True
            End If

            If warehouseID = reorderFromWarehouseID Then
                warehouse2Exists = True
            End If
        Next

        Return warehouse1Exists And warehouse2Exists
    End Function

    Private Function check_reorder_inputs_match() As Boolean

        ''This part checks each warehouse has a reorder information
        For Each warehouseID In Me.WarehouseInputs.Keys()
            Dim TempWarehouseInputs = Me.WarehouseInputs(warehouseID)
            Dim hasReorderInfo As Boolean = False

            If TempWarehouseInputs.Warehouse_SiteType = SiteType.Base_Warehouse Then
                hasReorderInfo = reorderPartnerExists(warehouseID, True)

            ElseIf TempWarehouseInputs.Warehouse_SiteType = SiteType.Dependent_Warehouse Then
                hasReorderInfo = reorderPartnerExists(warehouseID, False)
            End If

            If hasReorderInfo = False Then
                MessageBox.Show("Warehouse " & warehouseID & " has no proper reorder information")
                Return False
            End If
        Next

        '''This part checks that all reorder Inputs themselves make sense
        For Each reorder In Me.reorderInputs
            Dim allReorderInputsValid As Boolean = False
            If reorder.Item2 = "-1" Then
                allReorderInputsValid = checkBaseWarehouseExists(reorder.Item1)

            Else
                allReorderInputsValid = checkWarehousesExist(reorder.Item1, reorder.Item1)

            End If
            If allReorderInputsValid = False Then
                MessageBox.Show("Reorder relationship " & reorder.Item1 & " and " & reorder.Item2 & " is not valid")
                Return False
            End If

        Next

        Return True
    End Function

    Private Sub add_reorder_relationships(row As Object)
        Dim warehouse_id = Convert.ToString(row.Cells("Reorder_warehouse_ID").Value)
        Dim reordered_from_id = Convert.ToString(row.Cells("Reordered_from").Value)

        Dim reorderInput As New Reorder_inputs(
             lead_time_mean:=Convert.ToDouble(row.Cells("lead_time_mean").Value),
             lead_time_sd:=Convert.ToDouble(row.Cells("lead_time_std_dev").Value),
             reorder_cost:=Convert.ToDouble(row.Cells("reorder_cost").Value)
)
        reorderInputs.Add((warehouse_id, reordered_from_id, reorderInput))

    End Sub

    Private Sub SuggestionsListBox_DoubleClick(Sender As Object, e As EventArgs) Handles SuggestionsListBox.DoubleClick

        If SuggestionsListBox.SelectedItem IsNot Nothing Then
            SearchAddressTextBox.Text = SuggestionsListBox.SelectedItem.ToString
            Me.Suggested_input_chosen = True
            SuggestionsListBox.Items.Clear()
        End If
    End Sub


    Private Sub searchAddressTextBox_TextChanged(sender As Object, e As EventArgs) Handles SearchAddressTextBox.TextChanged
        If computeSuggestions.IsBusy Or String.IsNullOrEmpty(SearchAddressTextBox.Text) Then
            Return
        End If
        computeSuggestions.RunWorkerAsync(SearchAddressTextBox.Text)

    End Sub


    Private Sub computeSuggestions_DoWork(sender As Object, e As System.ComponentModel.DoWorkEventArgs) Handles computeSuggestions.DoWork
        'Debug.WriteLine("Compute Suggestions Do Work")
        Dim input As String = e.Argument.ToString()
        Try
            Dim request = New RestRequest($"maps/api/place/queryautocomplete/json?input={input}&key={googleAPIKey}", Method.Get)
            Dim response = Me.client.Execute(request).Content

            If response IsNot Nothing Then
                Dim deserialize As AutoCompleteResponce = JsonConvert.DeserializeObject(Of AutoCompleteResponce)(response)
                e.Result = deserialize
            End If

        Catch ex As Exception
            Debug.WriteLine(ex)
            e.Result = ex
        End Try
    End Sub

    Private Sub computeSuggestions_RunWorkerCompleted(sender As Object, e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles computeSuggestions.RunWorkerCompleted
        'Debug.WriteLine("Compute suggestions run work completed")
        If TypeOf (e.Result) Is Exception Then
            MessageBox.Show("Error fetching Suggestions")
            Return
        End If

        Dim suggestions As AutoCompleteResponce = e.Result

        If suggestions IsNot Nothing AndAlso suggestions.Predictions IsNot Nothing AndAlso suggestions.Predictions.Any() Then
            SuggestionsListBox.Items.Clear()
            For Each suggestion In suggestions.Predictions
                SuggestionsListBox.Items.Add(suggestion.description)
            Next
        End If
    End Sub

    Private Sub SubmitWarehouseBtn_Click(sender As Object, e As EventArgs) Handles SubmitWarehouseBtn.Click
        Dim WarehouseId As String
        Dim HoldingCost As Double
        Dim WarehouseType As String
        Try
            WarehouseId = Convert.ToInt32(WarehouseIDTextBox.Text)
            HoldingCost = Convert.ToDouble(HoldingCostTextBox.Text)
            If DependentWarehouseRadioButton.Checked = False And BaseWarehouseRadioButton.Checked = False Then
                MessageBox.Show("Please select a warehouse type")
                Return
            End If

            WarehouseType = If(DependentWarehouseRadioButton.Checked, "Dependent", "Base")
        Catch ex As Exception
            MessageBox.Show("Please enter a valid integer for the Warehouse ID and a number for the holding cost")
            Return
        End Try


        Dim Address As String = SearchAddressTextBox.Text
        If Suggested_input_chosen = False Then
            MessageBox.Show("Please select an address from the suggestions Box Below")
            Return
        End If

        If IdInLocationBox(WarehouseId) Then
            MessageBox.Show("Warehouse ID already exists")
            Return
        End If


        ''This then adds these values to the grid data view
        WarehouseDataGrid.Rows.Add(WarehouseId, HoldingCost, WarehouseType, Address)

        ''This then clears the text boxes
        SearchAddressTextBox.Clear()
        WarehouseIDTextBox.Clear()
        SuggestionsListBox.Items.Clear()
        HoldingCostTextBox.Clear()
        DependentWarehouseRadioButton.Checked = False
        BaseWarehouseRadioButton.Checked = False
        Me.Suggested_input_chosen = False

    End Sub

    Private Function IdInLocationBox(WarehouseID As String) As Boolean
        For Each row In WarehouseDataGrid.Rows()
            If row.Cells(0).Value = WarehouseID Then
                Return True
            End If
        Next
        Return False
    End Function

    Private Sub SubmitWarehouseLocationsBtn_Click(sender As Object, e As EventArgs) Handles SubmitWarehouseLocationsBtn.Click
        '''Updates the warehouseInputsData in this class
        UpdateWarehouseInputs()
        UpdateDatabaseWarehouseInputs()

        loadReorderInputstab()
        InputsTabControl.TabPages.Add(WarehouseRelationsTab)
        InputsTabControl.TabPages.Remove(WarehouseLocationsTab)
        InputsTabControl.SelectedTab = WarehouseRelationsTab

    End Sub


    Private Sub UpdateWarehouseInputs()
        Dim newWarehouseData = New Dictionary(Of String, Warehouse_independent_inputs)

        For Each row In WarehouseDataGrid.Rows()
            If Not row.IsNewRow Then
                Dim warehouse_id = Convert.ToString(row.Cells(0).Value)
                Dim holding_cost = Convert.ToDouble(row.Cells(1).Value)
                Dim site_type = If(row.Cells(2).Value = "Base", SiteType.Base_Warehouse, SiteType.Dependent_Warehouse)
                Dim address = row.Cells(3).Value.ToString()

                Dim warehouse_info = New Warehouse_independent_inputs(address, site_type, holding_cost)
                newWarehouseData.Add(warehouse_id, warehouse_info)
            End If
        Next

        Me.WarehouseInputs = newWarehouseData
    End Sub


    Private Sub loadWarehouseLocationsTab()
        If Me.WarehouseInputs IsNot Nothing Then
            For Each key In WarehouseInputs.Keys()
                Dim warehouseSiteType = If(WarehouseInputs(key).Warehouse_SiteType = SiteType.Base_Warehouse, "Base", "Dependent")
                WarehouseDataGrid.Rows.Add(key, WarehouseInputs(key).Holding_cost_per_pallet, warehouseSiteType,
                                             WarehouseInputs(key).Address)
            Next
            WarehouseDataGrid.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        End If
    End Sub

    ''Checks if the warehouse is already loaded 
    'Private Function WarehouseNotLoadedIn(key As Integer) As Boolean
    '    For Each row In WarehouseDataGrid.Rows()
    '        If row.Cells(0).Value = key Then
    '            Return False
    '        End If
    '    Next
    '    Return TrueS
    'End Function

    Private Sub InputsTabControl_SelectedIndexChanged(sender As Object, e As EventArgs) Handles InputsTabControl.SelectedIndexChanged

    End Sub

    Private Sub DeleteReorderRowButton_Click(sender As Object, e As EventArgs) Handles DeleteReorderRowButton.Click
        If DataGridReorders.SelectedRows.Count = 0 Then
            MessageBox.Show("Please Select a row to delete")
            Return
        End If

        Dim rowIndex As Integer = DataGridReorders.SelectedRows(0).Index
        Dim warehouseID As String = DataGridReorders.Rows(rowIndex).Cells(0).Value
        Dim ReorderFrom As String = DataGridReorders.Rows(rowIndex).Cells(1).Value
        Dim comfirmation As DialogResult = MessageBox.Show("Are you sure you want to delete the reorder relation between warehouse " & warehouseID & " and " & ReorderFrom & "?", "Delete Row", MessageBoxButtons.YesNo)

        If comfirmation = DialogResult.Yes Then
            DataGridReorders.Rows.RemoveAt(rowIndex)
        End If
    End Sub

    Private Sub FinaliseInputsButton_Click(sender As Object, e As EventArgs) Handles FinaliseInputsButton.Click
        Me.Close()
    End Sub

    Private Sub AlterInputsButton_Click(sender As Object, e As EventArgs) Handles AlterInputsButton.Click
        InputsTabControl.TabPages.Add(WarehouseLocationsTab)
        InputsTabControl.TabPages.Remove(ShowWarehouseTabPage)
        InputsTabControl.SelectedTab = WarehouseLocationsTab
    End Sub
End Class