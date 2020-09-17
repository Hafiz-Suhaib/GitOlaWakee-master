function ChangeLawyerStatus(id)
{
    $.ajax({
        type: "POST",
        url: '/Lawyer/ChangeLawyerStatus',
        data: {id: id },
        success: function (json) {
            if (json == "Success") {
                //swal("Updated!", "Lawyer Status Updated!", "success");
                window.location.reload();
            }


           // swal("Updated!", "Lawyer Status Updated!", "success");
            //window.location.reload();
        },
        error: function (xhr) {
            //  swal("Error", "Please Try again later!", "error");
        }
    });
}