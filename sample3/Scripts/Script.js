/// <reference path="jquery.jqGrid.js" />
$(function () {
    $("#jqGrid").jqGrid({
        url: "/Product/GetProducts",
        datatype: 'json',
        mtype: 'Get',
        colNames: ['Id', 'Name', 'Category', 'Price'],
        colModel: [
            { key: true, hidden: true, name: 'Id', index: 'Id', editable: true },
            { key: false, name: 'Name', index: 'Name', editable: true },
            { key: false, name: 'Category', index: 'Category', editable: true },
            { key: false, name: 'Price', index: 'Price', editable: true }],
        pager: jQuery('#jqControls'),
        rowNum: 10,
        rowList: [10, 20, 30, 40, 50],
        height: '100%',
        viewrecords: true,
        caption: 'Product Records',
        emptyrecords: 'No Products Records are Available to Display',
        jsonReader: {
            root: "rows",
            page: "page",
            total: "total",
            records: "records",
            repeatitems: false,
            Id: "0"
        },
        autowidth: true,
        multiselect: false,
    }).navGrid('#jqControls', {
        edit: true, add: false, del: false, search: true,
        searchtext: "Search Product", refresh: true
    },
        {
            zIndex: 100,
            url: '/Product/Edit',
            closeOnEscape: true,
            closeAfterEdit: true,
            recreateForm: true,
            afterComplete: function (response) {
                if (response.responseText) {
                    alert(response.responseText);
                }
            }
        },
        {
            zIndex: 100,
            url: "/Product/Create",
            closeOnEscape: true,
            closeAfterAdd: true,
            afterComplete: function (response) {
                if (response.responseText) {
                    alert(response.responseText);
                }
            }
        },
        {
            zIndex: 100,
            url: "/Product/Delete",
            closeOnEscape: true,
            closeAfterDelete: true,
            recreateForm: true,
            msg: "Are you sure you want to delete Product... ? ",
            afterComplete: function (response) {
                debugger;
                if (response.responseText) {
                    debugger;
                    alert(response.responseText);
                }
            }
        },
        {
            zIndex: 100,
            caption: "Search Product",
            sopt: ['cn']
        });
});


