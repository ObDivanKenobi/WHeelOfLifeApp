<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ContactModal.aspx.cs" Inherits="ContactModal" %>

<!DOCTYPE html>

<html lang="en">
    <head runat="server">
        <title>@l3ny</title>
        <link href="Css/Form.css" rel="stylesheet"/>
        <link href="Css/Modal.css" rel="stylesheet"/>
    </head>
    <body>
        <form id="form1" runat="server">
            <a href="#windowTitleDialog" data-toggle="modal">Contact me</a>
            <div id="windowTitleDialog" class="modal fade" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
                <div class="modal-dialog">
                    <div class="modal-content">
                        <div class="modal-header">
                            <button type="button" class="close" data-dismiss="modal" aria-hidden="true">×</button>
                            <h3 class="he">Contact Me.</h3>
                        </div>
                        <div id="formaContacto" class="fm_form modal-body">
                            <div id="divDialogElements">
                                <asp:Label runat="server" ID="lblName" AssociatedControlID="txtName" Text="Name" CssClass="fm_label"></asp:Label>
                                <div id="Nombre" class="fm_ringed">
                                    <asp:TextBox runat="server" ID="txtName" CssClass="fm_TextBox required"/>
                                </div>

                                <asp:Label runat="server" ID="lblEmail" AssociatedControlID="txtEmail" Text="Email" CssClass="fm_label"></asp:Label>
                                <div id="Correo" class="fm_ringed">
                                    <asp:TextBox CssClass="fm_TextBox required" ID="txtEmail" runat="server"/>
                                </div>

                                <asp:Label runat="server" ID="lblMessage" AssociatedControlID="txtMessage" Text="Message" CssClass="fm_label"></asp:Label>
                                <div id="Mensage" class="fm_ringed">
                                    <asp:TextBox runat="server" ID="txtMessage" CssClass="fm_Message required" TextMode="MultiLine" Rows="10"/>
                                </div>
                            </div>
                        </div>
                        <div class="fm_ringed modal-footer">
                            <asp:Button runat="server" ID="btnSubmit" Text="Send Message" OnClick="btnSubmit_Click" CssClass="fm_Button"/>&nbsp;&nbsp;&nbsp;

                            <asp:Button runat="server" ID="btnReset" Text="Reset" OnClick="Reset" CssClass="fm_Button"/>
                        </div>
                    </div>
                </div>
            </div>

            <!-- Large modal -->
            <a href="#large" class="btn btn-primary" data-toggle="modal" data-target=".bs-example-modal-lg">
                <button>Large modal</button>
            </a>

            <div id="#large" class="modal fade bs-example-modal-lg" tabindex="-1" role="dialog" aria-labelledby="myLargeModalLabel" aria-hidden="true">
                <div class="modal-dialog modal-lg">
                    <div class="modal-content">content...</div>
                </div>
            </div>

            <!-- Small modal -->
            <a href="#large" class="btn btn-primary" data-toggle="modal" data-target=".bs-example-modal-sm">
                <button>Small modal</button>
            </a>

            <div class="modal fade bs-example-modal-sm" tabindex="-1" role="dialog" aria-labelledby="mySmallModalLabel" aria-hidden="true">
                <div class="modal-dialog modal-sm">
                    <div class="modal-content">content...</div>
                </div>
            </div>
            <script type="text/javascript" src="http://code.jquery.com/jquery-2.1.1.min.js"></script>
            <script src="http://ajax.aspnetcdn.com/ajax/jquery.validate/1.11.1/jquery.validate.min.js"></script>
            <script src="Scripts/Modal.js"></script>
            <script>
                $("#form1").validate();
                $(document).ready(function () {
                    $('#windowTitleDialog').bind('show', function () {
                    });
                });
            </script>
        </form>
        <p>
            For more samples <a href="http://getbootstrap.com/javascript/#modals-sizes">Link text</a>
        </p>
    </body>
</html>
