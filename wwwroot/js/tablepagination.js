function pagination(tblID)
{
    $(tblID).DataTable({
        "pagingType": "simple_numbers" // "simple" option for 'Previous' and 'Next' buttons only
    });
    $('.dataTables_length').addClass('bs-select');
    $('.dataTables_length,.dataTables_info,.dataTables_paginate').show();
    $('.dataTables_length').css("margin-top", "12px");
}

function DealTablePagination(tblID) {
    $(tblID).DataTable({
        "pagingType": "simple_numbers", // "simple" option for 'Previous' and 'Next' buttons only
        "columnDefs": [{
            "targets": [0],
            "orderable": false
        }],
        "iDisplayLength": 25
    });
    $('.dataTables_length').addClass('bs-select');
    $('.dataTables_length,.dataTables_filter,.dataTables_info,.dataTables_paginate').show();
    $('.dataTables_length,.dataTables_filter').css("margin-top", "12px");
}