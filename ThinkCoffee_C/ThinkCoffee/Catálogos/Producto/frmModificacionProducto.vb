﻿Public Class frmModificacionProducto
    Dim filaSel As Integer

    Private Sub btnCancelar_Click(sender As Object, e As EventArgs) Handles btnCancelar.Click

    End Sub

    Private Sub btnSalir_Click(sender As Object, e As EventArgs) Handles btnSalir.Click
        conexionSql.Close()
        Me.Close()
    End Sub

    Private Sub frmModificacionProducto_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        conexionSql.Open()
        Dim n As Integer

        comando.CommandText = "Select tlb_categoria.nombre from tlb_categoria"
        lector = comando.ExecuteReader
        While lector.Read
            cboCategoria.Items.Add(lector(0))
        End While
        lector.Close()


        comando.CommandText = "Select count(*) from tlb_producto"
        n = comando.ExecuteScalar + 1

        If n > 1 Then
            comando.CommandText = "Select tlb_producto.idProducto, tlb_producto.nombre from tlb_producto "
            lector = comando.ExecuteReader

            While lector.Read
                dgModificacion.Rows.Add(lector(0), lector(1))
            End While
            lector.Close()
        Else
        End If

    End Sub

    Private Sub txtNombreP_TextChanged(sender As Object, e As EventArgs) Handles txtNombreP.TextChanged
        dgModificacion.Rows.Clear()
        comando.CommandText = "Select tlb_producto.idProducto, tlb_producto.nombre from tlb_producto WHERE nombre LIKE '%" & txtNombreP.Text & "%' "
        lector = comando.ExecuteReader

        While lector.Read
            dgModificacion.Rows.Add(lector(0), lector(1))
        End While
        lector.Close()
    End Sub

    Private Sub btnModificar_Click(sender As Object, e As EventArgs) Handles btnModificar.Click
        txtNombreP.ReadOnly = True
        dgModificacion.Enabled = True

        cboCategoria.Enabled = True
    End Sub

    Private Sub dgModificacion_SelectionChanged(sender As Object, e As EventArgs) Handles dgModificacion.SelectionChanged
        filaSel = dgModificacion.CurrentRow.Index


        'comando.CommandText = "select tlb_producto.idProducto, tlb_producto.idReceta, tlb_producto.idCategoria, tlb_producto.nombre, tlb_producto.precio, tlb_producto.imagen from tlb_producto where tlb_producto.idProducto = " & dgModificacion(0, filaSel).Value & ""
        'lector = comando.ExecuteReader
        'lector.Read()
        'txtIdProducto.Text = lector(0)
        'lector.Close()
    End Sub

    Private Sub dgModificacion_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgModificacion.CellClick
        comando.CommandText = "select tlb_producto.idProducto, tlb_producto.idReceta, tlb_producto.idCategoria, tlb_producto.nombre, tlb_producto.precio, tlb_producto.imagen from tlb_producto where tlb_producto.idProducto = " & dgModificacion(0, filaSel).Value & ""
        lector = comando.ExecuteReader
        lector.Read()
        txtIdProducto.Text = lector(0)
        txtIdReceta.Text = lector(1)
        txtIdCategoria.Text = lector(2)
        txtNombre.Text = lector(3)
        txtPrecio.Text = lector(4)
        ptbImagen.Image = lector(5)
        lector.Close()
    End Sub
End Class