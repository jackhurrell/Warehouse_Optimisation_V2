Public Class MyResults
    Public Sub New()
        results = New List(Of Result)
    End Sub
    Public Property results As List(Of Result)
    Public Property status As String
End Class

Public Class Result

    Public Sub New()
        address_components = New List(Of AddressComponents)
        types = New List(Of String)
        geometry = New GeometryData
    End Sub
    Public Property address_components As List(Of AddressComponents)
    Public Property formatted_address As String
    Public Property geometry As GeometryData
    Public Property partial_match As Boolean
    Public Property types As List(Of String)

End Class

Public Class AddressComponents
    Public Sub New()
        types = New List(Of String)
    End Sub
    Public Property long_name As String
    Public Property short_name As String
    Public Property types As List(Of String)
End Class

Public Class GeometryData
    Public Property bounds As Coords
    Public Property location As LatLong
    Public Property location_type As String
    Public Property viewport As Coords
End Class

Public Class Coords
    Public Property northeast As LatLong
    Public Property southwest As LatLong
End Class

Public Class LatLong
    Public Property lat As String
    Public Property lng As String
End Class
