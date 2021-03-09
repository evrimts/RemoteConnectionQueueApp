var dataTable;
const table = document.querySelector('#DT_load');
var usrName = table.dataset.user;

$(document).ready(function () {
    loadDataTable();
});

function loadDataTable() {
    dataTable = $('#DT_load').DataTable({
        "ajax": {
            "url": "/connections/getall/",
            "type": "GET",
            "datatype": "json"
        },
        "columns": [
            { "data": "musteriAdi", "width": "20%" },
            { "data": "sunucuIp", "width": "15%" },
            { "data": "sunucuKullaniciAdi", "width": "15%" },
            { "data": "bagliKisi", "width": "15%" },
            { "data": "baglantiZamani", "width": "0%" },
            {
                data: 'baglantiZamani',
                render: function (data, type, row, meta) {
                    console.log(row.musteriAdi);
                    console.log(data);
                    if (data == null) {
                        return null;
                    } else {
                        var baglantiZamani = new Date(data);
                        console.log(baglantiZamani);

                        var today = new Date();
                        console.log(today);
                        let msDifference = Math.abs(today - baglantiZamani);
                        console.log(msDifference);
                        let minutes = Math.floor(msDifference / 1000 / 60);
                        console.log(minutes);
                        return minutes;
                    }
                }, "width": "10%"

            },
            { "data": "note", "width": "15%" },
            {
                data: 'bagliKisi',
                render: function (data, type, row, meta) {
                    if (data === null) {
                        return `<div class="text-center">
                        <a href="/connections/Connect?id=${row.id}" class='btn btn-success text-white' style='cursor:pointer; width:120px; margin:3px;'>
                            Bağlan
                        </a >
                        <a href="/connections/JoinQueue?id=${row.id}" class='btn btn-info text-white' style='cursor:pointer; width:120px;'>
                            Sıra İşlemleri
                        </a>
                        </div>`
                    } else if (data == usrName) {
                        return `<div class="text-center">
                        <a class='btn btn-danger text-white' style='cursor:pointer; width:120px;'
                            onclick=Disconnect('/connections/Disconnect?id='+${row.id})>
                            Ayrıl
                        </a >
                        </div>`
                    } else {
                        return `<div class="text-center">
                        <a href="/connections/JoinQueue?id=${row.id}" class='btn btn-info text-white' style='cursor:pointer; width:120px;'>
                            Sıra İşlemleri
                        </a>
                        </div>`
                    }
                    ;
                }, "width": "10%"
            }
        ],
        "language": {
            "emptyTable": "Kayıt yok"
        },
        "width": "100%"
    });
}

function Delete(url) {
    swal({
        title: "Are you sure?",
        text: "Once deleted, you will not be able to recover",
        icon: "warning",
        buttons: true,
        dangerMode: true
    }).then((willDelete) => {
        if (willDelete) {
            $.ajax({
                type: "DELETE",
                url: url,
                success: function (data) {
                    if (data.success) {
                        toastr.success(data.message);
                        dataTable.ajax.reload();
                    }
                    else {
                        toastr.error(data.message);
                    }
                }
            });
        }
    });
}

function Connect(url) {
    swal({
        title: "Sunucuya Bağlanıyorsunuz",
        text: "Emin misiniz?",
        icon: "warning",
        buttons: true,
        dangerMode: true
    }).then((willConnect) => {
        if (willConnect) {
            $.ajax({
                type: "POST",
                url: url,
                success: function (data) {
                    if (data.success) {
                        toastr.success(data.message);
                        dataTable.ajax.reload();
                    }
                    else {
                        toastr.error(data.message);
                    }
                }
            });
        }
    });
}

function Disconnect(url) {
    swal({
        title: "Sunucudan Ayrılıyorsunuz",
        text: "Emin misiniz?",
        icon: "warning",
        buttons: true,
        dangerMode: true
    }).then((willDisConnect) => {
        if (willDisConnect) {
            $.ajax({
                type: "POST",
                url: url,
                success: function (data) {
                    if (data.success) {
                        toastr.success(data.message);
                        dataTable.ajax.reload();
                    }
                    else {
                        toastr.error(data.message);
                    }
                }
            });
        }
    });
}