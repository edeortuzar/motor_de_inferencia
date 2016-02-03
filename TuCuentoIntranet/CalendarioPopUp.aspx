<%@ Page Language="C#" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>

<script runat="server">
    protected void Change_Date(System.Object sender, System.EventArgs e)
    {
        if (Request.QueryString["destino"] != "")
        {
            string strScript =
                "<script>window.opener.document.getElementById('" +
                Request.QueryString["destino"].ToString() + "').value = '" +
                CalPopup.SelectedDate.ToString("dd/MM/yyyy") +
                "';self.close()" +
                "</" + "script>";
            RegisterClientScriptBlock("Calendar_ChangeDate", strScript);
        }
    }
</script>

<body style="margin: 0px;">
    <form id="Form1" runat="server">
    <asp:Calendar ID="CalPopup" OnSelectionChanged="Change_Date" runat="server" BackColor="White"
        Width="220px" DayNameFormat="Shortest" ForeColor="#003399" Height="200px"
        Font-Size="8pt" Font-Names="Verdana" BorderColor="#3366CC" 
        BorderWidth="1px" CellPadding="1">
        <TodayDayStyle ForeColor="White" BackColor="#99CCCC"></TodayDayStyle>
        <SelectorStyle BackColor="#99CCCC" ForeColor="#336666"></SelectorStyle>
        <NextPrevStyle Font-Size="8pt" ForeColor="#CCCCFF"></NextPrevStyle>
        <DayHeaderStyle Height="1px" BackColor="#99CCCC" ForeColor="#336666"></DayHeaderStyle>
        <SelectedDayStyle Font-Bold="True" BackColor="#009999" ForeColor="#CCFF99"></SelectedDayStyle>
        <TitleStyle Font-Size="10pt" Font-Bold="True" ForeColor="#CCCCFF" 
            BackColor="#003399" BorderColor="#3366CC" BorderWidth="1px" Height="25px">
        </TitleStyle>
        <WeekendDayStyle BackColor="#CCCCFF" />
        <OtherMonthDayStyle ForeColor="#999999"></OtherMonthDayStyle>
    </asp:Calendar>
    </form>
</body>
</html>
