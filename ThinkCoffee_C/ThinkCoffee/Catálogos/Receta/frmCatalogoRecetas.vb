﻿Public Class frmCatalogoRecetas
    '***********************************************************************************************************************************************
    '****************************************************   APARTIR DE AQUI LUCY WAS HERE  '********************************************
    Dim entra As Boolean = False
    Private Sub frmCatalogoRecetas_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        conexionSql.Open()
        entra = False
    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles btnBuscar.Click
        Dim insumos As New frmAgregarInsumo
        insumos.dgInsumos.SelectionMode = DataGridViewSelectionMode.FullRowSelect

        Dim ban As Boolean = False
        insumos.ShowDialog()

        If insumos.dgInsumos.CurrentRow IsNot Nothing Then
            Dim fila As Integer = insumos.dgInsumos.CurrentRow.Index
            txtIdInsumo.Text = insumos.dgInsumos(0, fila).Value
            txtInsumo.Text = insumos.dgInsumos(1, fila).Value
            txtUnidadM.Text = insumos.dgInsumos(2, fila).Value
            txtExistencia.Text = insumos.dgInsumos(3, fila).Value

            btnAgregar.Enabled = True
            txtCantidad.Focus()
            txtCantidad.Enabled = True
        Else
            MessageBox.Show("NO SE HA SELECCIONADO INSUMO", "FALTA DE INFORMACIÓN", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End If


    End Sub

    Private Sub btnSalir_Click(sender As Object, e As EventArgs) Handles btnSalir.Click
        conexionSql.Close()
        Me.Close()
    End Sub

    Private Sub btnNuevo_Click(sender As Object, e As EventArgs) Handles btnNuevo.Click
        entra = False
        Dim n As Integer
        comando.CommandText = "Select count(*) from tlb_receta"
        n = comando.ExecuteScalar + 1

        txtIdReceta.Text = n
        btnNuevo.Enabled = False
        btnSalir.Enabled = False
        btnGrabar.Enabled = True
        btnCancelar.Enabled = True
        txtNombre.Enabled = True
        btnBuscar.Enabled = True

        txtNombre.Focus()


    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles btnAgregar.Click
        If cmbUnidadM.Text = "" Or String.IsNullOrWhiteSpace(txtCantidad.Text) Then
            MessageBox.Show("NO SE HA INGRESADO UNIDAD DE MEDIDA", "FALTA DE INFORMACIÒN", MessageBoxButtons.OK, MessageBoxIcon.Error)
            cmbUnidadM.Focus()
            txtCantidad.Focus()
        Else
            If Not IsNumeric(txtCantidad.Text) Then
                MessageBox.Show("CANTIDAD NO VÁLIDA", "VERIFICAR DATOS INGRESADOS", MessageBoxButtons.OK, MessageBoxIcon.Error)
                txtCantidad.Focus()
            Else
                dgReceta.Rows.Add(txtIdInsumo.Text, txtInsumo.Text, txtCantidad.Text, cmbUnidadM.Text)
                txtExistencia.Text = ""
                txtIdInsumo.Text = ""
                txtInsumo.Text = ""
                txtUnidadM.Text = ""
                txtCantidad.Text = ""
                cmbUnidadM.Text = ""

                btnAgregar.Enabled = False
                btnBuscar.Focus()
                entra = True
            End If
        End If

    End Sub

    Private Sub btnGrabar_Click(sender As Object, e As EventArgs) Handles btnGrabar.Click
        If entra Then
            comando.CommandText = "Insert into tlb_receta (idReceta, nombre) values(" & Val(txtIdReceta.Text) & ",'" & txtNombre.Text & "'" & ")"
            comando.ExecuteNonQuery()


            For x = 0 To dgReceta.RowCount - 1
                comando.CommandText = "Insert into tlb_detReceta (idReceta, idInsumo, cantidad, unidadM) values(" & Val(txtIdReceta.Text) & ", " & CInt(dgReceta(0, x).Value) & ", '" & CDec(dgReceta(2, x).Value) & "','" & dgReceta(3, x).Value & "')"
                comando.ExecuteNonQuery()
            Next

            txtIdReceta.Text = ""
            txtNombre.Text = ""
            dgReceta.Rows.Clear()

            mensajeGrabar()

            btnNuevo.Enabled = True
            btnSalir.Enabled = True
            btnGrabar.Enabled = False
            btnCancelar.Enabled = True
            btnAgregar.Enabled = False
            txtCantidad.Enabled = False
            btnBuscar.Enabled = False
            txtNombre.Enabled = False
            cmbUnidadM.Enabled = False
            entra = False

        Else
            MessageBox.Show("ES NECESARIO PRESIONAR EL BOTÒN DE ACEPTAR PARA REGISTRAR RECETA", "ERROR DE REGISTRO", MessageBoxButtons.OK, MessageBoxIcon.Error)
            entra = False
        End If



    End Sub

    Private Sub txtCantidad_TextChanged(sender As Object, e As EventArgs) Handles txtCantidad.TextChanged
        Dim suma As Decimal
        suma = 0.00
        txtCantidad.Text = suma + Val(txtCantidad.Text)

    End Sub

    Private Sub btnCancelar_Click(sender As Object, e As EventArgs) Handles btnCancelar.Click
        btnNuevo.Enabled = True
        btnSalir.Enabled = True
        btnGrabar.Enabled = False
        btnCancelar.Enabled = True
        btnAgregar.Enabled = False
        txtCantidad.Enabled = False
        btnBuscar.Enabled = False
        txtNombre.Enabled = False
        btnCancelar.Enabled = False
        cmbUnidadM.Enabled = False


        txtIdReceta.Text = ""
        txtNombre.Text = ""

        txtIdInsumo.Text = ""
        txtExistencia.Text = ""
        txtInsumo.Text = ""
        txtUnidadM.Text = ""
        txtCantidad.Text = ""

        dgReceta.Rows.Clear()

    End Sub

    Private Sub txtNombre_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtNombre.KeyPress
        e.KeyChar = UCase(e.KeyChar)
        If e.KeyChar > ChrW(26) Then
            If InStr(CadenaValida, e.KeyChar) = 0 Then
                e.KeyChar = ChrW(0)
            End If
        End If
        cmbUnidadM.Enabled = True
    End Sub

    Private Sub cmbUnidadM_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbUnidadM.SelectedIndexChanged

    End Sub
End Class