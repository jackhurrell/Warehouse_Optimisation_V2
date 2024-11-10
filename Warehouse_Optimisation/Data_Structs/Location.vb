Public Class Location

    Public Address As String
    Public latitude As Double
    Public longitude As Double

    Public Sub New(Address As String, latitude As Double, longitude As Double)
        Me.Address = Address
        Me.latitude = latitude
        Me.longitude = longitude
    End Sub

End Class
