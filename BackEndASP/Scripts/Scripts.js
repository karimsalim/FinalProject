$(document).ready(function () {
    $("#CloseDiv").click(function () {
        $("#modalEditSuccess").fadeOut();
    });

    //$(".Delete").click(function (e) {
    //    console.log($(".Delete").prev().val());
    //    alert($(".Delete").prev().val());
    //    $.post(
    //        "/Accounts/Delete",
    //        {
    //            id: $(".idAccount").val(),
    //        },
    //        function (data) {
    //            console.log(data);
    //            //return;
    //            $mymodal = $("#myModals");
    //            //update the modal's body with the response received
    //            $mymodal.find("div.modal-body").html(data);
    //            // Show the modal
    //            $mymodal.fadeIn();
    //        });
    //});

    $('#FormDel').submit(function (e) {
        e.preventDefault();
        var $form = $(this)[0].attributes["action"].value;
        console.table($form);
        $.post(
            $form,
            {
            },
            function (data) {
                console.log(data);
                //return;
                $mymodal = $("#myModals");
                //update the modal's body with the response received
                $mymodal.find("div.modal-body").html(data);
                // Show the modal
                $mymodal.fadeIn();
            });
    });

    
    

    ////$.post($form.attr("action"), $form.serialize()).done(function (res) {
    ////    $mymodal = $("#myModals");
    ////    //update the modal's body with the response received
    ////    $mymodal.find("div.modal-body").html(res);
    ////    // Show the modal
    ////    $mymodal.modal("show");
    ////});
    //});
});

function CloseModalDel() {
    $("#myModals").fadeOut();
}
