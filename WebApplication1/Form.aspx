<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Form.aspx.cs" Inherits="WebApplication1.Form" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:Label ID="Label3" runat="server" Text="ЛЕО Кинопоиск топ 3000" Font-Bold="True" Font-Size="Large" Font-Underline="False"></asp:Label>
        <br />
        <br />
        <asp:Label ID="Label1" runat="server" Text="Фильм"></asp:Label>
        <asp:TextBox ID="TextFilm" runat="server"></asp:TextBox>
        <br />
        <asp:Label ID="Label2" runat="server" Text="Жанр"></asp:Label>
        <asp:TextBox ID="TextGenre" runat="server"></asp:TextBox>
        <br />
        <br />
        <asp:Button ID="BtnAdd" runat="server" Text="Добавить" OnClick="BtnAdd_Click" />
        <asp:Button ID="BtnUpdate" runat="server" Text="Изменить" OnClick="BtnUpdate_Click" />
        <asp:Button ID="BtnDel" runat="server" Text="Удалить" OnClick="BtnDel_Click" />
        <br />
        <br />
        <asp:Label ID="Label4" runat="server"></asp:Label>
        <br />
        <br />
        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="false">
            <Columns>
                <asp:BoundField DataField="Film" HeaderText="Фильм:" />
                <asp:BoundField DataField="Genre" HeaderText="Жанр:" />
                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:LinkButton ID="lnkselect" runat="server" CommandArgument='<%# Eval("ID") %>' OnCommand="lnkselect_click">Выбрать</asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
        <br />
        <br />
        <asp:Button ID="BtnClr" runat="server" Text="Отменить выбор строки" OnClick="BtnClr_Click" />
    </div>
    </form>
</body>
</html>
