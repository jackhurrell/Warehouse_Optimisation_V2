Imports Newtonsoft.JSON


Public Class AutoCompleteResponce

    Public Predictions As List(Of Prediction)


End Class

Public Class prediction

    Public description As String

    <JsonProperty(PropertyName:="place_id")>
    Public place_id As String

End Class
