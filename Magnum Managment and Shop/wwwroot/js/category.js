//var btn = document.getElementById("deletebtn");
//var deleteAJAX= document.getElementById("delete-ajax")

//btn.addEventListener("click", function () {
//    var myRequest = new XMLHttpRequest();

//    myRequest.open('POST', 'https://localhost:44345/Admin/Category/Delete?categoryId=' + myData)
//    myRequest.onload = function () {
//        var myData = myRequest.responseText;
//        renderHTML(myData);
//    };
//    myRequest.send();
//});

//function renderHTML(data) {
//    var htmlString = "";
//    for (i = 0; i < data.length; i++) {
//        htmlString += "Successfully deleted"
//    }
//}

//deleteAJAX.insertAdjacentHTML('beforeend', htmlString);












//var dataTable;

//$(document).ready(function () {
//    LoadDataTable();
//})

//function loadDataTable() {
//    dataTable = $('#myTable').DataTable({
//        ajax: { url: '/admin/category/delete' }
//    });
//}

//if (success == true) {
//    Swal.fire({
//        title: 'Are you sure?',
//        text: "You won't be able to revert this!",
//        icon: 'warning',
//        showCancelButton: true,
//        confirmButtonColor: '#3085d6',
//        cancelButtonColor: '#d33',
//        confirmButtonText: 'Yes, delete it!'
//    }).then((result) => {
//        if (result.isConfirmed) {
//            $.ajax({
//                url: url,
//                type: 'DELETE',
//                success: function (data) {
//                    dataTable.ajax.reload();
//                    toastr.success(data.message);
//                }
//            })
//        }
//    })
//}