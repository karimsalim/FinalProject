﻿$(document).ready(function () {

    $('#FormDelCard').submit(function (e) {
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
});

function CloseModalDel() {
    $("#myModals").fadeOut();
}

