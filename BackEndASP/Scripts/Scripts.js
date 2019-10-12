$(document).ready(function () {
    $("#CloseDiv").click(function () {
        $("#modalEditSuccess").fadeOut();
    });

    $('.FormDelSaving').submit(function (e) {
        e.preventDefault();
        var $form = $(this)[0].attributes["action"].value;
        console.table($form);
        $.post(
            $form,
            {
            },
            function (data) {
                //console.log(data);
                //return;
                $mymodal = $("#myModals");
                //update the modal's body with the response received
                $mymodal.find("div.modal-body").html(data);
                // Show the modal
                $mymodal.fadeIn();
            });
    });

    $('.FormDelDeposit').submit(function (e) {
        e.preventDefault();
        var $form = $(this)[0].attributes["action"].value;
        console.table($form);
        $.post(
            $form,
            {
            },
            function (data) {
                //console.log(data);
                //return;
                $mymodal = $("#myModals");
                //update the modal's body with the response received
                $mymodal.find("div.modal-body").html(data);
                // Show the modal
                $mymodal.fadeIn();
            });
    });

    $('.FormDelCard').submit(function (e) {
        e.preventDefault();
        var $form = $(this)[0].attributes["action"].value;
        $.post(
            $form,
            {},
            function (data) {
                $mymodal = $("#myModals");
                $mymodal.find("div.modal-body").html(data);
                $mymodal.fadeIn();
            }
        );
    });
});

function CloseModalDel() {
    $("#myModals").fadeOut();
}