Imports System.Messaging

<ComClass(MessageQueue.ClassId, MessageQueue.InterfaceId, MessageQueue.EventsId)> _
Public Class MessageQueue

    Dim mFormatName As String = "FormatName:DIRECT=OS:.\private$\LZWMessageQueue"
    Dim mLastErrMsg As String
    Dim mIntCount As Integer = 10
    Dim mFltCount As Integer = 5
    Dim mStrCount As Integer = 5
    Dim mInts(mIntCount) As Integer
    Dim mFlts(mFltCount) As Double
    Dim mStrs(mStrCount) As String
    Dim mEventHost As String
    Dim mEventQueue As String
    Dim mEventType As Integer
    Dim mEventCounter As Integer
    Dim mEventTime As String
    Dim mEventTimeType As Integer
    Dim mDataQuality As Integer
    Dim mWaitedFor As Integer
    Dim mEventDetected As String
    Dim mTriggerTime As String
    Dim mMessage As String

#Region "COM GUIDs"
    ' These  GUIDs provide the COM identity for this class 
    ' and its COM interfaces. If you change them, existing 
    ' clients will no longer be able to access the class.
    Public Const ClassId As String = "51e35653-5c55-4086-bc79-0318c5498346"
    Public Const InterfaceId As String = "fcf3767d-6db9-4ca2-bc6d-cda8aa77a1cf"
    Public Const EventsId As String = "e38bab2b-e628-4b2e-b82c-a05bfe60d34f"
#End Region

    ' A creatable COM class must have a Public Sub New() 
    ' with no parameters, otherwise, the class will not be 
    ' registered in the COM registry and cannot be created 
    ' via CreateObject.
    Public Sub New()
        MyBase.New()
        DefineProdMsg(mIntCount, mFltCount, mStrCount)
        mEventTimeType = 1 'Local date and time
    End Sub
    ''' <summary>
    ''' Defines the sizes for an LZWMessageQueue message.
    ''' </summary>
    ''' <param name="nIntegerCount">The number of Integers in the message.</param>
    ''' <param name="nFloatCount">The number of Floats in the message.</param>
    ''' <param name="nStringCount">The number of Strings in the message.</param>
    ''' <returns>True for success, False for error</returns>
    ''' <remarks>The default sizes are 6 integers, 4 floats and 4 strings, use this method to change these.</remarks>
    Public Function DefineProdMsg(ByVal nIntegerCount As Integer, ByVal nFloatCount As Integer, ByVal nStringCount As Integer) As Boolean
        Try
            mIntCount = nIntegerCount
            mFltCount = nFloatCount
            mStrCount = nStringCount
            ReDim mInts(mIntCount)
            ReDim mFlts(mFltCount)
            ReDim mStrs(mStrCount)
            Dim x As Integer
            For x = 1 To mIntCount
                mInts(x) = 0
            Next
            For x = 1 To mFltCount
                mFlts(x) = 0
            Next
            For x = 1 To mStrCount
                mStrs(x) = ""
            Next
            DefineProdMsg = True
        Catch ex As Exception
            mLastErrMsg = ex.Message
            DefineProdMsg = False
        End Try
    End Function
    ''' <summary>
    ''' Sets the value of an LZWMessageQueue Message Integer
    ''' </summary>
    ''' <param name="nIndex">The index of the Integer in the message.</param>
    ''' <param name="nValue">The value to set.</param>
    ''' <returns>True for success, False for error</returns>
    ''' <remarks>If the method fails use LastErrorMsg() to find the reason.</remarks>
    Public Function SetInteger(ByVal nIndex As Integer, ByVal nValue As Integer) As Boolean
        Try
            If nIndex > mIntCount Or nIndex = 0 Then
                SetInteger = False
            Else
                mInts(nIndex) = nValue
                SetInteger = True
            End If
        Catch ex As Exception
            mLastErrMsg = ex.Message
            SetInteger = False
        End Try
    End Function
    ''' <summary>
    '''Sets the value of an LZWMessageQueue Message Integer
    ''' </summary>
    ''' <param name="nIndex">The index of the Float in the message.</param>
    ''' <param name="fValue">The value to set.</param>
    ''' <returns>True for success, False for error</returns>
    ''' <remarks>If the method fails use LastErrorMsg() to find the reason.</remarks>
    Public Function SetFloat(ByVal nIndex As Integer, ByVal fValue As Double) As Boolean
        Try
            If nIndex > mFltCount Or nIndex = 0 Then
                SetFloat = False
            Else
                mFlts(nIndex) = fValue
                SetFloat = True
            End If
        Catch ex As Exception
            mLastErrMsg = ex.Message
            SetFloat = False
        End Try
    End Function
    ''' <summary>
    '''Sets the value of an LZWMessageQueue Message Integer
    ''' </summary>
    ''' <param name="nIndex">The index of the String in the message.</param>
    ''' <param name="sValue">The value to set.</param>
    ''' <returns>True for success, False for error</returns>
    ''' <remarks>If the method fails use LastErrorMsg() to find the reason.</remarks>
    Public Function SetString(ByVal nIndex As Integer, ByVal sValue As String) As Boolean
        Try
            If nIndex > mStrCount Or nIndex = 0 Then
                SetString = False
            Else
                mStrs(nIndex) = sValue
                SetString = True
            End If
        Catch ex As Exception
            mLastErrMsg = ex.Message
            SetString = False
        End Try
    End Function
    ''' <summary>
    ''' Send your own message to the MessageQueue also specifying a label for the message.
    ''' </summary>
    ''' <param name="sLabel">A label for the message.</param>
    ''' <param name="sBody">The text of the message, this should be valid XML.</param>
    ''' <returns>True for success, False for error.</returns>
    ''' <remarks>Use this if you want to format your own XML messages and send them on the queue.</remarks>
    Public Function Send(ByVal sLabel As String, ByVal sBody As String) As Boolean
        Dim oQueue As System.Messaging.MessageQueue
        Dim oMessage As System.String

        Try
            oQueue = New System.Messaging.MessageQueue
            oQueue.Path = mFormatName
            oMessage = sBody
            oQueue.Send(oMessage, sLabel)
            Send = True
            mMessage = oMessage
        Catch ex As Exception
            mLastErrMsg = ex.Message
            Send = False
            mMessage = oMessage
        End Try
    End Function
    ''' <summary>
    ''' Sends an LZWMessageQueue Message to the queue specified by the QueueName property.
    ''' The message size can be defined using the DefineProdMsg method.
    ''' The message values can be added using the SetInteger, SetFloat and SetString methods.
    ''' </summary>
    ''' <returns>True for success, False for error</returns>
    ''' <remarks>If the method fails use LastErrorMsg() to find the reason.</remarks>
    Public Function SendProdMsg() As Boolean
        Dim oQueue As System.Messaging.MessageQueue
        Dim oMessage As System.String
        Dim x As Integer

        Try
            'Build up our message
            oMessage = "<LZWProductionEvent>"
            oMessage = oMessage + "<EventHost>" + mEventHost + "</EventHost>"
            oMessage = oMessage + "<EventQueue>" + mEventQueue + "</EventQueue>"
            oMessage = oMessage + "<EventType>" + mEventType.ToString() + "</EventType>"
            oMessage = oMessage + "<EventCounter>" + mEventCounter.ToString() + "</EventCounter>"
            oMessage = oMessage + "<EventTime>" + mEventTime + "</EventTime>"
            oMessage = oMessage + "<DataQuality>" + mDataQuality.ToString() + "</DataQuality>"
            oMessage = oMessage + "<WaitedFor>" + mWaitedFor.ToString() + "</WaitedFor>"
            oMessage = oMessage + "<EventDetected>" + mEventDetected + "</EventDetected>"
            oMessage = oMessage + "<TriggerTime>" + mTriggerTime + "</TriggerTime><Ints>"
            For x = 1 To mIntCount
                oMessage = oMessage + "<Int" + x.ToString() + ">" + mInts(x).ToString() + "</Int" + x.ToString() + ">"
            Next
            oMessage = oMessage + "</Ints><Floats>"
            For x = 1 To mFltCount
                oMessage = oMessage + "<Float" + x.ToString() + ">" + mFlts(x).ToString() + "</Float" + x.ToString() + ">"
            Next
            oMessage = oMessage + "</Floats><Strings>"
            For x = 1 To mStrCount
                oMessage = oMessage + "<String" + x.ToString() + ">" + mStrs(x).ToString() + "</String" + x.ToString() + ">"
            Next
            oMessage = oMessage + "</Strings></LZWProductionEvent>"

            oQueue = New System.Messaging.MessageQueue
            oQueue.Path = mFormatName

            oQueue.Send(oMessage, "LZWProductionEvent")
            SendProdMsg = True
            mMessage = oMessage
        Catch ex As Exception
            mLastErrMsg = ex.Message
            SendProdMsg = False
            mMessage = oMessage
        End Try
    End Function
    ''' <summary>
    ''' Get or Set the name of the LZWMessageQueue Message Queue.
    ''' The default value is "FormatName:DIRECT=OS:.\private$\APVProduction"
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property QueueName() As String
        Get
            Return mFormatName
        End Get
        Set(ByVal sQueueName As String)
            mFormatName = sQueueName
        End Set
    End Property
    ''' <summary>
    ''' Get the number of Integers defined in the LZWMessageQueue Message.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property IntegerCount() As Integer
        Get
            Return mIntCount
        End Get
    End Property
    ''' <summary>
    ''' Get the number of Floats defined in the LZWMessageQueue Message.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property FloatCount() As Integer
        Get
            Return mFltCount
        End Get
    End Property
    ''' <summary>
    ''' Get the number of Strings defined in the LZWMessageQueue Message.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property StringCount() As Integer
        Get
            Return mStrCount
        End Get
    End Property
    ''' <summary>
    ''' The name of the PC sending the message.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks>For information only.</remarks>
    Public Property EventHost() As String
        Get
            Return mEventHost
        End Get
        Set(ByVal sValue As String)
            mEventHost = sValue
        End Set
    End Property
    ''' <summary>
    ''' The name of the Queue on the host.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks>For information only.</remarks>
    Public Property EventQueue() As String
        Get
            Return mEventQueue
        End Get
        Set(ByVal sValue As String)
            mEventQueue = sValue
        End Set
    End Property
    ''' <summary>
    ''' The Event Type, this identifies the message and how the contents are handled by the database.
    ''' For example 1=CIP
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property EventType() As Integer
        Get
            Return mEventType
        End Get
        Set(ByVal nValue As Integer)
            mEventType = nValue
        End Set
    End Property
    ''' <summary>
    ''' An incrementing counter attached to the event.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks>For information only.</remarks>
    Public Property EventCounter() As Integer
        Get
            Return mEventCounter
        End Get
        Set(ByVal nValue As Integer)
            mEventCounter = nValue
        End Set
    End Property
    ''' <summary>
    ''' The date and time of the event message. This is used by the database to timestamp the data.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property EventTime() As String
        Get
            Return mEventTime
        End Get
        Set(ByVal sValue As String)
            mEventTime = sValue
        End Set
    End Property
    ''' <summary>
    ''' How to interpret the EventTime property.
    ''' 1=Local Date and time
    ''' 2=UTC Date and time
    ''' 3=Unix Seconds
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks>The database uses this value to work out how to convert the EventTime value into UTC date and time.</remarks>
    Public Property EventTimeType() As String
        Get
            Return mEventTimeType
        End Get
        Set(ByVal nValue As String)
            mEventTimeType = nValue
        End Set
    End Property
    ''' <summary>
    ''' Indication of the quality of the event data.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks>For information only.</remarks>
    Public Property DataQuality() As Integer
        Get
            Return mDataQuality
        End Get
        Set(ByVal nValue As Integer)
            mDataQuality = nValue
        End Set
    End Property
    ''' <summary>
    ''' Indication of how long the data collector waited for the all the data in the message.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks>For information only.</remarks>
    Public Property WaitedFor() As Integer
        Get
            Return mWaitedFor
        End Get
        Set(ByVal nValue As Integer)
            mWaitedFor = nValue
        End Set
    End Property
    ''' <summary>
    ''' Time all the event data was received by the collector.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks>For information only.</remarks>
    Public Property EventDetected() As String
        Get
            Return mEventDetected
        End Get
        Set(ByVal sValue As String)
            mEventDetected = sValue
        End Set
    End Property
    ''' <summary>
    ''' Time the data collector detected the trigger for this event.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks>For information only.</remarks>
    Public Property TriggerTime() As String
        Get
            Return mTriggerTime
        End Get
        Set(ByVal sValue As String)
            mTriggerTime = sValue
        End Set
    End Property
    ''' <summary>
    ''' The last error message that occurred when calling the methods and properties of this object.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property LastErrorMsg() As String
        Get
            Return mLastErrMsg
        End Get
    End Property

    Public ReadOnly Property EMessage() As String
        Get
            Return mMessage
        End Get
    End Property
End Class


