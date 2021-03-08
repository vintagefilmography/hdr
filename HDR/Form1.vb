Imports TIS.Imaging

Public Class Form1
    Dim VideoHasStarted As Boolean
    Dim StartToggle As Integer = 0
    Dim ZoomOutValue As Decimal = 1.2
    Dim TriggerToggle As Integer = 0
    Dim ImgSaveToggle As Integer = 0
    Dim BitToggle As Integer = 0
    Dim TiffToggle As Integer = 0
    Dim impath1 As String
    Dim impath2 As String
    Dim imFullpath1 As String = "C:\"
    Dim imFullpath2 As String = "C:\"
    Dim IncrementLow As Decimal = 0
    Dim ImageCount As Integer = 0
    Dim ExposureProperty As VCDRangeProperty
    Dim ExposureAuto As VCDSwitchProperty
    Dim ImageOdd As Integer = 0
    Dim ImageEven As Integer = 0


    Private _imageSink As TIS.Imaging.FrameSnapSink
    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        On Error GoTo err_Form_Load
        If Not IcImagingControl1.DeviceValid Then
            IcImagingControl1.ShowDeviceSettingsDialog()

            If Not IcImagingControl1.DeviceValid Then
                MsgBox("No device was selected.")

                Exit Sub
            End If
        End If
        IcImagingControl1.LiveDisplay = True
        IcImagingControl1.LiveCaptureLastImage = False
        IcImagingControl1.LiveCaptureContinuous = True


        IcImagingControl1.Width = IcImagingControl1.ImageWidth
        IcImagingControl1.Height = IcImagingControl1.ImageHeight
        'IcImagingControl1.MemoryCurrentGrabberColorformat = ICImagingControlColorformats.ICRGB64
        IcImagingControl1.MemoryCurrentGrabberColorformat = ICImagingControlColorformats.ICRGB32

        'use this for tiff images
        '_imageSink = New TIS.Imaging.FrameSnapSink(TIS.Imaging.MediaSubtypes.RGB64)
        'IcImagingControl1.Sink = _imageSink
        'IcImagingControl1.ImageAvailableExecutionMode = 1

        Exit Sub

err_Form_Load:
        MsgBox(Err.Description)
    End Sub
    Private Sub ShowBuffer(buffer As IFrameQueueBuffer)
        Try
            IcImagingControl1.DisplayImageBuffer(buffer)
        Catch
            MessageBox.Show("snap image failed, timeout occurred.")
        End Try
    End Sub






    Private Sub IcImagingControl1_Load(sender As Object, e As EventArgs) Handles IcImagingControl1.Load

    End Sub

    Private Sub Start_Click(sender As Object, e As EventArgs) Handles Start.Click
        If StartToggle = 0 Then
            Start.BackColor = Color.Red
            StartToggle = 1
            IcImagingControl1.LiveStart()
            VideoHasStarted = True
        Else
            Start.BackColor = Color.White
            StartToggle = 0
            IcImagingControl1.LiveStop()
            VideoHasStarted = False
        End If
    End Sub

    Private Sub zoomout_click(sender As Object, e As EventArgs) Handles ZoomOut.Click
        IcImagingControl1.LiveDisplayDefault = 0
        IcImagingControl1.LiveDisplayZoomFactor = IcImagingControl1.LiveDisplayZoomFactor / ZoomOutValue
    End Sub

    Private Sub zoomin_click(sender As Object, e As EventArgs) Handles ZoomIn.Click
        IcImagingControl1.LiveDisplayDefault = 0
        IcImagingControl1.LiveDisplayZoomFactor = IcImagingControl1.LiveDisplayZoomFactor * ZoomOutValue
    End Sub

    Private Sub settings_click(sender As Object, e As EventArgs) Handles Settings.Click
        IcImagingControl1.ShowPropertyDialog()
    End Sub

    Private Sub trigger_click(sender As Object, e As EventArgs) Handles Trigger.Click
        If TriggerToggle = 0 Then
            Trigger.BackColor = Color.Red
            TriggerToggle = 1
            IcImagingControl1.DeviceTrigger = True
        Else
            Trigger.BackColor = Color.White
            TriggerToggle = 0
            IcImagingControl1.DeviceTrigger = False
        End If
    End Sub

    Private Sub imgsave_click(sender As Object, e As EventArgs) Handles ImgSave.Click
        If ImgSaveToggle = 0 Then
            ImgSave.BackColor = Color.Red
            ImgSaveToggle = 1
        Else
            ImgSave.BackColor = Color.White
            ImgSaveToggle = 0
        End If

    End Sub

    Private Sub path1_click(sender As Object, e As EventArgs) Handles Path1.Click
        If impath1 = "" Then
            Dim dialog As New FolderBrowserDialog()
            dialog.RootFolder = Environment.SpecialFolder.Desktop
            dialog.SelectedPath = "c:\"
            dialog.Description = "select application configeration files path"
            If dialog.ShowDialog() = Windows.Forms.DialogResult.OK Then
                impath1 = dialog.SelectedPath
            End If
            'my.computer.filesystem.writealltext(impath & "apppath.txt", impath, false)
            Debug.Print(impath1)
        End If
    End Sub

    Private Sub path2_click(sender As Object, e As EventArgs) Handles Path2.Click
        If impath2 = "" Then
            Dim dialog As New FolderBrowserDialog()
            dialog.RootFolder = Environment.SpecialFolder.Desktop
            dialog.SelectedPath = "c:\"
            dialog.Description = "select application configeration files path"
            If dialog.ShowDialog() = Windows.Forms.DialogResult.OK Then
                impath2 = dialog.SelectedPath
            End If
            'my.computer.filesystem.writealltext(impath & "apppath.txt", impath, false)
            Debug.Print(impath2)
        End If
    End Sub

    Private Sub inclow_click(sender As Object, e As EventArgs) Handles IncLow.Click
        IncrementLow = IncrementLow + 0.1
        MessageBox.Show("low exposure =" + Str(IncrementLow))
    End Sub

    Private Sub declo_click(sender As Object, e As EventArgs) Handles DecLo.Click
        IncrementLow = IncrementLow - 0.1
        MessageBox.Show("low exposure =" + Str(IncrementLow))
    End Sub

    Private Sub icimagingcontrol1_imageavailable(sender As Object, e As ICImagingControl.ImageAvailableEventArgs) Handles IcImagingControl1.ImageAvailable

        On Error GoTo err_imageavailable_handler

        Dim CurrentBuffer As ImageBuffer
        Dim DisplayBuffer As ImageBuffer

        'snap(Capture) an image To the memory
        'Dim image As TIS.Imaging.IFrameQueueBuffer
        'IcImagingControl1.LiveStop()
        'IcImagingControl1.Sink = _imageSink
        'Try
        'image = _imageSink.SnapSingle(TimeSpan.FromSeconds(1))
        'Catch
        'MessageBox.Show("snap image failed, timeout occurred.")
        'End Try
        'image.SaveAsTiff("d:\hawkeye\hdr\grab3.tiff")
        'Dim buffer_type As Integer
        'buffer_type = IcImagingControl1.ImageActiveBuffer.BitsPerPixel
        'buffer_type = IcImagingControl1.Sink.SinkType
        CurrentBuffer = IcImagingControl1.ImageActiveBuffer
        'IcImagingControl1.DisplayImageBuffer(CurrentBuffer)
        'save images. odd in path1 and even in path2
        If ImgSaveToggle = 1 Then
            ExposureProperty = IcImagingControl1.VCDPropertyItems.FindInterface(VCDGUIDs.VCDID_Exposure, VCDGUIDs.VCDElement_Value, VCDGUIDs.VCDInterface_Range)
            ExposureAuto = IcImagingControl1.VCDPropertyItems.FindInterface(VCDGUIDs.VCDID_Exposure, VCDGUIDs.VCDElement_Auto, VCDGUIDs.VCDInterface_Switch)
            ImageCount = ImageCount + 1
            If ImageCount And 1& Then
                ExposureAuto.Switch = False
                ExposureProperty.Value = ExposureProperty.Value - IncrementLow
                ImageOdd = ImageOdd + 1
                impath1 = impath1.Trim(vbNullChar)
                If TiffToggle = 1 Then
                    imFullpath1 = impath1 + "\" + Str(ImageOdd) + ".tiff"
                Else
                    imFullpath1 = impath1 + "\" + Str(ImageOdd) + ".jpg"
                End If
                imFullpath1 = imFullpath1.Replace(" ", "")
                If TiffToggle = 1 Then
                    CurrentBuffer.SaveAsTiff(imFullpath1)
                Else
                    CurrentBuffer.SaveAsJpeg(imFullpath1, 100)
                End If
                'CurrentBuffer.SaveImage(imFullpath1)
                'MessageBox.Show("stop odd")
            Else
                ExposureProperty.Value = ExposureProperty.Value + IncrementLow
                ExposureAuto.Switch = True
                ImageEven = ImageEven + 1
                impath2 = impath2.Trim(vbNullChar)
                If TiffToggle = 1 Then
                    imFullpath2 = impath2 + "\" + Str(ImageEven) + ".tiff"
                Else
                    imFullpath2 = impath2 + "\" + Str(ImageEven) + ".jpg"
                End If
                imFullpath2 = imFullpath2.Replace(" ", "")
                If TiffToggle = 1 Then
                    CurrentBuffer.SaveAsTiff(imFullpath2)
                Else
                    CurrentBuffer.SaveAsJpeg(imFullpath2, 100)
                End If
                'CurrentBuffer.SaveImage(imFullpath2)
                'MessageBox.Show("stop even")
            End If
        End If
        Debug.Print("image count=" + CStr(ImageCount))
        Debug.Print("imageodd=" + Str(ImageOdd))
        Debug.Print("imageeven=" + Str(ImageEven))
        Debug.Print(imFullpath1)
        Debug.Print(imFullpath2)
        'catch
        'messagebox.show("snap image failed, timeout occurred.")
        'end try
err_imageavailable_handler:
        Debug.Print(Err.Description)
    End Sub

    Private Sub SaveConf_Click(sender As Object, e As EventArgs) Handles SaveConf.Click
        IcImagingControl1.SaveDeviceStateToFile("device_state.txt")
    End Sub

    Private Sub LoadConf_Click(sender As Object, e As EventArgs) Handles LoadConf.Click
        IcImagingControl1.LoadDeviceStateFromFile("device_state.txt", True)
    End Sub

    Private Sub Bit64_Click(sender As Object, e As EventArgs) Handles Bit64.Click
        If BitToggle = 0 Then
            Bit64.BackColor = Color.Red
            BitToggle = 1
            IcImagingControl1.MemoryCurrentGrabberColorformat = ICImagingControlColorformats.ICRGB64
        Else
            Bit64.BackColor = Color.White
            BitToggle = 0
            IcImagingControl1.MemoryCurrentGrabberColorformat = ICImagingControlColorformats.ICRGB24
        End If
    End Sub

    Private Sub SaveTiff_Click(sender As Object, e As EventArgs) Handles SaveTiff.Click
        If TiffToggle = 0 Then
            SaveTiff.BackColor = Color.Red
            TiffToggle = 1
        Else
            SaveTiff.BackColor = Color.White
            TiffToggle = 0
        End If
    End Sub
End Class
