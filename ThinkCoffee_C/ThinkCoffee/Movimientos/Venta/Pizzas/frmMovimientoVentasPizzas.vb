﻿Public Class frmMovimientoVentasPizzas

    Dim selec As Integer = 0
    Dim ubicación As Integer = 0
    Private Sub frmMovimientoVentasPizzas_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        conexionSql.Open()

        cboTamaño.SelectedIndex = 0
    End Sub

    Private Sub btnSalir_Click(sender As Object, e As EventArgs)
        Me.Close()

    End Sub
    Private Sub cboTamaño_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboTamaño.SelectedIndexChanged
        If cboTamaño.Text = "REBANADA" Then
            limpiarLista()
            limite = 2

            cboTipo.SelectedIndex = 0
            cboTipo.Enabled = False
            txtPrecio.Text = "35.00"

            CheckedListBox1.Enabled = True

        ElseIf cboTamaño.Text = "CHICA" Then
            limpiarLista()
            cboTipo.Enabled = True
            txtPrecio.Text = ""


        ElseIf cboTamaño.Text = "GRANDE" Then
            limpiarLista()
            cboTipo.Enabled = True
            txtPrecio.Text = ""

        End If
    End Sub

    Private Sub CheckedListBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles CheckedListBox1.SelectedIndexChanged

        If buscarCheck() >= limite Then
            MsgBox("No sé pueden ingresar más toppings")
            limpiarSeleccionado(CheckedListBox1.SelectedItem.ToString())
        End If
    End Sub

    Private Sub btnCrear_Click(sender As Object, e As EventArgs) Handles btnCrear.Click
        comando.CommandText = "select count(tlb_venta.idVenta) from tlb_venta;"
        Dim n As Integer = comando.ExecuteScalar + 1
        txtIdVenta.Text = n
        btnGrabar.Enabled = True
        txtCantidad.Enabled = True
        cboTamaño.Enabled = True


    End Sub

    Private Sub btnGrabar_Click(sender As Object, e As EventArgs) Handles btnGrabar.Click
        If String.IsNullOrWhiteSpace(txtPrecio.Text) Then
            MessageBox.Show("No se ha ingresado precio", "FALTA DE INFORMACIÓN", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            txtPrecio.Focus()
        Else
            comando.CommandText = "Insert into tlb_venta(idVenta, tipo, fecha, total) values(" & CInt(txtIdVenta.Text) & ", '" & "PIZZA" & "', '" & dtpFecha.Value.Date & "', " & CDec(txtPrecio.Text) & ")"
            comando.ExecuteNonQuery()

            For x = 0 To 20
                If CheckedListBox1.GetItemCheckState(x) = 1 Then
                    comando.CommandText = "Insert into tlb_pizza(idVenta, size,	topping) values(" & CInt(txtIdVenta.Text) & ", '" & cboTamaño.Text & "', '" & CheckedListBox1.Items(x).ToString() & "')"
                    comando.ExecuteNonQuery()
                End If
            Next
            mensajeGrabar()
        End If



    End Sub

    Private Sub cboTipo_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboTipo.SelectedIndexChanged
        If cboTamaño.Text = "CHICA" And cboTipo.Text = "NORMAL" Then
            limpiarLista()
            txtPrecio.Text = "100.00"
            limite = 2

        ElseIf cboTamaño.Text = "GRANDE" And cboTipo.Text = "NORMAL" Then
            limpiarLista()
            limite = 2
            txtPrecio.Text = "180.00"

        ElseIf cboTamaño.Text = "CHICA" And cboTipo.Text = "MIXTA" Then
            limpiarLista()
            limite = 5
            txtPrecio.Text = "120.00"

        ElseIf cboTamaño.Text = "GRANDE" And cboTipo.Text = "MIXTA" Then
            limpiarLista()
            limite = 9
            txtPrecio.Text = "230.00"

        End If
    End Sub

    Private Sub cboTamaño_SelectedValueChanged(sender As Object, e As EventArgs) Handles cboTamaño.SelectedValueChanged
        If cboTamaño.Text = "REBANADA" Then
            limpiarLista()
            limite = 2

            cboTipo.SelectedIndex = 0
            cboTipo.Enabled = False
            txtPrecio.Text = "35.00"

            CheckedListBox1.Enabled = True

        ElseIf cboTamaño.Text = "CHICA" Then
            limpiarLista()
            cboTipo.Enabled = True
            txtPrecio.Text = ""


        ElseIf cboTamaño.Text = "GRANDE" Then
            limpiarLista()
            cboTipo.Enabled = True
            txtPrecio.Text = ""

        End If
    End Sub
End Class