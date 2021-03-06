// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

// Write your JavaScript code.
$.ajaxSetup({
    contentType: "application/json",
    dataType: 'json',
});

const Guid = {
    Empty: '00000000-0000-0000-0000-000000000000'
}

const NDH = {
    GetData: (wrapper) => {
        let inputs = wrapper.find('input, textarea, select');
        let data = {};
        inputs.each((i, e) => {
            let name = $(e).prop('name');
            let value = $(e).val();
            let type = $(e).prop('type');
            if (name !== '') {
                data[name] = $(e).val();
            }

            if (type === 'checkbox') {
                data[name] = $(e).is(':checked');
            }

        });
        return data;
    },
    GetJsData: (wrapper) => {
        //배열 처리 안한 임시 데이터

        let MakeData = (_split_name, e) => {
            let value = $(e).val();
            let type = $(e).prop('type');
            let _data = {};

            if (_split_name.length > 1) {
                let key = _split_name[0];
                _data[key] = MakeData(_split_name.splice(1, _split_name.length), e)
            } else {

                if (type === 'radio') {
                    _data[_split_name] = $(e).is(':checked');
                } else if (type === 'checkbox') {

                    //switch면 ?
                    let is_switch = $(e).parent().hasClass('custom-switch');

                    if (is_switch) {
                        _data[_split_name] = $(e).is(':checked');
                    } else {
                        _data[_split_name[0]] = $(e).is(':checked');
                    }
                }

                else if (type === 'hidden') {

                    //이미 같은 데이터 들어있으면 안해야됨(체크박스 땜에)
                    //문제있음
                    if (_data[_split_name[0]] !== undefined) {
                        _data[_split_name[0]] = value;
                    }
                }
                else {

                    let data_prop = $(e).data('type');
                    if (data_prop === 'number') {
                        _data[_split_name[0]] = parseInt(value);
                    } else {
                        _data[_split_name[0]] = value;
                    }
                }
            }
            return _data;
        }
        let InitArrayData = (data) => {
            for (let key in data) {
                let is_array = key.indexOf('[') !== -1 && key.indexOf(']') !== -1;
                if (is_array) {
                    let array_name = key.substring(0, key.indexOf('['));
                    data[array_name] = [];
                }
                if (typeof (data[key]) === 'object') {
                    data[key] = InitArrayData(data[key]);
                }
            }

            return data;
        }
        //만든 배열에 넣고 지움
        let ProcessData = (data) => {
            for (let key in data) {
                if (typeof (data[key]) === 'object') {

                    data[key] = ProcessData(data[key]);
                }
                let is_array = key.indexOf('[') !== -1 && key.indexOf(']') !== -1;
                if (is_array) {
                    let array_name = key.substring(0, key.indexOf('['));
                    let array_index = key.substring(key.indexOf('[') + 1, key.indexOf(']'));
                    data[array_name][array_index] = data[key];
                    delete data[key];
                }
            }
            return data;
        }
        let inputs = wrapper.find('input, textarea, select');
        let data = {};

        inputs.each((i, e) => {
            let name = $(e).prop('name');
            let value = $(e).val();
            let type = $(e).prop('type');
            if (name !== '') {

                //아예 네임 자체를 배열로
                let split_name = name.split('.');
                let single = MakeData(split_name, $(e));
                //data = $.extend({}, data, single);
                data = $.extend(true, data, single);

            }
        });
        data = InitArrayData(data);
        data = ProcessData(data);
        return data;
    },
    SetData: (wrapper, data) => {
        let inputs = wrapper.find('input, textarea, select');
        inputs.each((i, e) => {
            let name = $(e).prop('name');
            let value = $(e).val();
            let type = $(e).prop('type');
            if (name !== '') {
                $(e).val(data[name]);
            }
        })

    },
    SetJsData: (wrapper, data) => {

        let FindValue = (full_name, split_name, d) => {
            let value;
            let first_name = split_name[0];

            if (first_name === undefined) {
                return value;
            }

            let is_array = first_name.indexOf('[') !== -1 && first_name.indexOf(']') !== -1;
            if (is_array) {
                let array_name = first_name.substring(0, first_name.indexOf('['));
                let array_index = first_name.substring(first_name.indexOf('[') + 1, first_name.indexOf(']'));
                let arr_data = d[array_name][array_index];
                value = FindValue(full_name, split_name.slice(1, split_name.length), d[array_name][array_index]);
            } else {
                if (d[first_name] == null) { return '';}
                if (typeof (d[first_name]) === 'object') {
                    //여기 문제
                    value = FindValue(full_name, split_name.slice(1, split_name.length), d[first_name]);
                } else {
                    value = d[first_name];
                }
            }
            return value;
        }

        let inputs = wrapper.find('input, textarea, select');
        inputs.each((i, e) => {
            let name = $(e).prop('name');
            let type = $(e).prop('type');
            if (name !== '') {
                //아예 네임 자체를 배열로
                let split_name = name.split('.');
                let value = FindValue(split_name, split_name, data)

                if (type === 'radio') {
                    ///value가 같은놈을 찾아서 체크 해야함

                    let default_value = $(e).val();

                    if (default_value === 'true') {
                        default_value = true;
                    } else if (default_value === 'false') {
                        default_value = false;
                    }
                    $(e).prop('checked', default_value === value);

                } else {
                    $(e).val(value);
                }


            }
        });
    },
    MakeFormTag: (data, prefix) => {

        let prefix_name = prefix.join('.')

        if (prefix_name !== '') {
            prefix_name += '.';
        }

        let result = '';
        let html = '';

        for (let key in data) {

            if (data[key] === undefined) {
                continue;
            }

            html = ''

            if (Array.isArray(data[key])) {
                data[key].forEach((e, i) => {
                    html += NDH.MakeFormTag(e, [...prefix, `${key}[${i}]`]);
                })

            } else {

                if (typeof (data[key]) === 'object') {
                    html += NDH.MakeFormTag(data[key], [...prefix, key]);
                } else {
                    html += `<input type="hidden" name="${prefix_name + key}" value="${data[key]}"/>`;
                }
            }

            result += html;
        }

        return result;
    },
    InitHtml: (wrapper) => {
        let inputs = wrapper.find('input, textarea');

        $(inputs).each(function (i, e) {
            let type = $(e).prop('type');
            if (type === 'text' || type === 'hidden') {
                $(e).prop('disabled', false);
                $(e).val('');
            } else if (type === 'checkbox') {
                $(e).prop('checked', false);
            } else if (type === 'radio') {
            }
            else if (type === 'file') {
                $(e).val('');
            }
            else {
                $(e).val('');
            }
        })


        //input 초기화
        let enables = wrapper.find('input, textarea, select, button');
        enables.each(function (i, e) {
            $(e).prop('disabled', false);
        });
    },
    SetSelectListItem: (element, data) => {

        $(element).html('');

        data.forEach(e => {
            $(element).append(`<option value="${e.value}" data-hasChild="">${e.text} </option>`)
        })
    },
    DisableInput: (wrapper) => {
        let inputs = wrapper.find('input');

        inputs.each((i, e) => {
            $(e).prop('disabled', true);
        })
    },
    FindArrayObject: function (array, value) {
        let result = array.filter(function (e, i) {
            if (e.value == value) {
                return e;
            }
        });

        if (result.length > 0) {
            return result[0];
        } else {
            return null;
        }
    },
    CheckStillOpenModal: function () {
        let showing_modal = $('body .modal.show');

        if (showing_modal.length > 0) {
            $('body').addClass('modal-open');
        }
    },
    MakeExcel: function (file_name, data) {

        //
        /*
         * 스크립트 2개 추가할것
         * https://cdnjs.cloudflare.com/ajax/libs/xlsx/0.15.5/xlsx.full.min.js
         * https://cdnjs.cloudflare.com/ajax/libs/FileSaver.js/1.3.8/FileSaver.min.js
        */

        var wb = XLSX.utils.book_new();

        // step 2. 시트 만들기
        var newWorksheet = XLSX.utils.aoa_to_sheet(data);

        // step 3. workbook에 새로만든 워크시트에 이름을 주고 붙인다.
        XLSX.utils.book_append_sheet(wb, newWorksheet, 'Sheet1');

        // step 4. 엑셀 파일 만들기
        var wbout = XLSX.write(wb, { bookType: 'xlsx', type: 'binary' });

        // step 5. 엑셀 파일 내보내기
        saveAs(new Blob([NDH.S2AB(wbout)], { type: "application/octet-stream" }), `${file_name}.xlsx`);

    },
    S2AB: function (s) {
        var buf = new ArrayBuffer(s.length); //convert s to arrayBuffer
        var view = new Uint8Array(buf);  //create uint8array as viewer
        for (var i = 0; i < s.length; i++) view[i] = s.charCodeAt(i) & 0xFF; //convert to octet
        return buf;
    },
}

let ECORMMERCE_ARRAY = {
    ShopeeDiscountStatus: [
        { text: 'UPCOMING', value: "UPCOMING", order: 1, color: '#c73b0f', class: 'secondary' },
        { text: 'ONGOING', value: "ONGOING", order: 2, color: '#c73b0f', class: 'secondary' },
        { text: 'EXPIRED', value: "EXPIRED", order: 3, color: '#c73b0f', class: 'secondary' },
        { text: 'ALL', value: "ALL", order: 4, color: '#c73b0f', class: 'secondary' },
    ]
};

const ECORMMERCE_ARRAY_KEYS = Object.keys(ECORMMERCE_ARRAY);

let ArrayBuilder = function (key) {

    let array = ECORMMERCE_ARRAY[key];
    if (array === undefined) {
        return undefined;
    }

    let parent = {
        text: undefined,
        value: undefined,
        order: undefined,
        color: undefined,
        class: undefined,
        description: '',
    }

    let result = array.map(function (e, i, arr) {
        let temp = $.extend({}, parent, e);
        return temp;
    }).sort(function (a, b) {
        return a.order < b.order ? -1 : a.order > b.order ? 1 : 0
    });

    return result;
}


if (typeof(Swal) !== 'undefined') {

    Swal = Swal.mixin({
        confirmButtonText: '확인',
        cancelButtonText: '취소',
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
    })

}


const CurrencyPair = [
    {Currency: 'IDR', Country: 'ID'},
    { Currency: 'SGD', Country: 'SG' },
    { Currency: 'MYR', Country: 'MY' },
    { Currency: 'PHP', Country: 'PH' },
    { Currency: 'TWD', Country: 'TW' },
    { Currency: 'THB', Country: 'TH' },
    { Currency: 'VND', Country: 'VN' }
]


const CountryLanguagePair = [
    {Language: 'en', Country: 'ID',},
    {Language: 'en', Country: 'SG',},
    {Language: 'en', Country: 'MY',},
    {Language: 'en', Country: 'PH',},
    {Language: 'zh', Country: 'TW',},
    {Language: 'th', Country: 'TH',},
    { Language: 'en', Country: 'VN', }
]


//공통 함수들

//모달 여러개 떠있을 때 닫으면 focus 안가는거, Body에 클래스가 토글되니까 확인할것
$(document).on('hidden.bs.modal', '.modal', function () {
    $('.modal-backdrop').remove();
});
$(document).on('hidden.bs.modal', '.modal', NDH.CheckStillOpenModal);

if($.fn.DataTable !== undefined){
    $.extend($.fn.DataTable.defaults, {
        "aaData": null,
        "aaSorting": [
            [
                0,
                "asc"
            ]
        ],
        "aaSortingFixed": [],
        "ajax": null,
        "aLengthMenu": [
            15,
            30,
            50,
            100
        ],
        "aoColumns": null,
        "aoColumnDefs": null,
        "aoSearchCols": [],
        "asStripeClasses": null,
        "bAutoWidth": true,
        "bDeferRender": false,
        "bDestroy": false,
        "bFilter": true,
        "bInfo": true,
        "bJQueryUI": false,
        "bLengthChange": true,
        "bPaginate": true,
        "bProcessing": false,
        "bRetrieve": false,
        "bScrollCollapse": false,
        "bServerSide": false,
        "bSort": true,
        "bSortMulti": true,
        "bSortCellsTop": false,
        "bSortClasses": true,
        "bStateSave": false,
        "fnCreatedRow": null,
        "fnDrawCallback": null,
        "fnFooterCallback": null,
        "fnHeaderCallback": null,
        "fnInfoCallback": null,
        "fnInitComplete": null,
        "fnPreDrawCallback": null,
        "fnRowCallback": null,
        "fnServerData": null,
        "fnServerParams": null,
        "fnStateLoadParams": null,
        "fnStateLoaded": null,
        "fnStateSaveParams": null,
        "iStateDuration": 7200,
        "iDeferLoading": null,
        "iDisplayLength": 15,
        "iDisplayStart": 0,
        "iTabIndex": 0,
        "oClasses": {
            "_hungarianMap": {}
        },
        "oLanguage": {
            "oAria": {
                "sSortAscending": ": activate to sort column ascending",
                "sSortDescending": ": activate to sort column descending",
                "_hungarianMap": {
                    "sortAscending": "sSortAscending",
                    "sortDescending": "sSortDescending"
                }
            },
            "oPaginate": {
                "sFirst": "처음",
                "sLast": "마지막",
                "sNext": "다음",
                "sPrevious": "이전",
                "_hungarianMap": {
                    "first": "처음",
                    "last": "마지막",
                    "next": "이전",
                    "previous": "다음"
                }
            },
            "sEmptyTable": "조회된 자료가 없습니다.",
            "sInfo": "_START_ 에서 _END_ 까지 총 _TOTAL_ 개 항목.",
            "sInfoEmpty": "항목이 없습니다.",
            "sInfoFiltered": "(전체 _MAX_개 항목에서 검색됨)",
            "sInfoPostFix": "",
            "sDecimal": "",
            "sThousands": ",",
            "sLengthMenu": "_MENU_ 개씩 보기",
            "sLoadingRecords": "데이터를 불러오는 중입니다...",
            "sProcessing": "데이터를 처리하는 중입니다...",
            "sSearch": "검색 :",
            "sSearchPlaceholder": "",
            "sUrl": "",
            "sZeroRecords": "일치하는 항목이 없습니다.",
            "_hungarianMap": {
                "aria": "oAria",
                "paginate": "oPaginate",
                "emptyTable": "sEmptyTable",
                "info": "sInfo",
                "infoEmpty": "sInfoEmpty",
                "infoFiltered": "sInfoFiltered",
                "infoPostFix": "sInfoPostFix",
                "decimal": "sDecimal",
                "thousands": "sThousands",
                "lengthMenu": "sLengthMenu",
                "loadingRecords": "sLoadingRecords",
                "processing": "sProcessing",
                "search": "sSearch",
                "searchPlaceholder": "sSearchPlaceholder",
                "url": "sUrl",
                "zeroRecords": "sZeroRecords"
            }
        },
        "oSearch": {
            "bCaseInsensitive": true,
            "sSearch": "",
            "bRegex": false,
            "bSmart": true,
            "_hungarianMap": {
                "caseInsensitive": "bCaseInsensitive",
                "search": "sSearch",
                "regex": "bRegex",
                "smart": "bSmart"
            }
        },
        "sAjaxDataProp": "data",
        "sAjaxSource": null,
        "sDom": "lfrtip",
        "searchDelay": null,
        "sPaginationType": "simple_numbers",
        "sScrollX": "",
        "sScrollXInner": "",
        "sScrollY": "",
        "sServerMethod": "GET",
        "renderer": "bootstrap",
        "rowId": "DT_RowId",
        "_hungarianMap": {
            "data": "aaData",
            "sorting": "aaSorting",
            "sortingFixed": "aaSortingFixed",
            "lengthMenu": "aLengthMenu",
            "columns": "aoColumns",
            "columnDefs": "aoColumnDefs",
            "searchCols": "aoSearchCols",
            "stripeClasses": "asStripeClasses",
            "autoWidth": "bAutoWidth",
            "deferRender": "bDeferRender",
            "destroy": "bDestroy",
            "filter": "bFilter",
            "info": "bInfo",
            "jQueryUI": "bJQueryUI",
            "lengthChange": "bLengthChange",
            "paginate": "bPaginate",
            "processing": "bProcessing",
            "retrieve": "bRetrieve",
            "scrollCollapse": "bScrollCollapse",
            "serverSide": "bServerSide",
            "sort": "bSort",
            "sortMulti": "bSortMulti",
            "sortCellsTop": "bSortCellsTop",
            "sortClasses": "bSortClasses",
            "stateSave": "bStateSave",
            "createdRow": "fnCreatedRow",
            "drawCallback": "fnDrawCallback",
            "footerCallback": "fnFooterCallback",
            "formatNumber": "fnFormatNumber",
            "headerCallback": "fnHeaderCallback",
            "infoCallback": "fnInfoCallback",
            "initComplete": "fnInitComplete",
            "preDrawCallback": "fnPreDrawCallback",
            "rowCallback": "fnRowCallback",
            "serverData": "fnServerData",
            "serverParams": "fnServerParams",
            "stateLoadCallback": "fnStateLoadCallback",
            "stateLoadParams": "fnStateLoadParams",
            "stateLoaded": "fnStateLoaded",
            "stateSaveCallback": "fnStateSaveCallback",
            "stateSaveParams": "fnStateSaveParams",
            "stateDuration": "iStateDuration",
            "deferLoading": "iDeferLoading",
            "displayLength": "iDisplayLength",
            "displayStart": "iDisplayStart",
            "tabIndex": "iTabIndex",
            "classes": "oClasses",
            "language": "oLanguage",
            "search": "oSearch",
            "ajaxDataProp": "sAjaxDataProp",
            "ajaxSource": "sAjaxSource",
            "dom": "sDom",
            "paginationType": "sPaginationType",
            "scrollX": "sScrollX",
            "scrollXInner": "sScrollXInner",
            "scrollY": "sScrollY",
            "serverMethod": "sServerMethod"
        },
        "column": {
            "aDataSort": null,
            "iDataSort": -1,
            "asSorting": [
                "asc",
                "desc"
            ],
            "bSearchable": true,
            "bSortable": true,
            "bVisible": true,
            "fnCreatedCell": null,
            "mData": null,
            "mRender": null,
            "sCellType": "td",
            "sClass": "",
            "sContentPadding": "",
            "sDefaultContent": null,
            "sName": "",
            "sSortDataType": "std",
            "sTitle": null,
            "sType": null,
            "sWidth": null,
            "_hungarianMap": {
                "dataSort": "iDataSort",
                "sorting": "asSorting",
                "searchable": "bSearchable",
                "sortable": "bSortable",
                "visible": "bVisible",
                "createdCell": "fnCreatedCell",
                "data": "mData",
                "render": "mRender",
                "cellType": "sCellType",
                "class": "sClass",
                "contentPadding": "sContentPadding",
                "defaultContent": "sDefaultContent",
                "name": "sName",
                "sortDataType": "sSortDataType",
                "title": "sTitle",
                "type": "sType",
                "width": "sWidth"
            }
        },
        "dom": "<'row'<'col-sm-12 col-md-6'l><'col-sm-12 col-md-6'f>><'row'<'col-sm-12'tr>><'row'<'col-sm-12 col-md-5'i><'col-sm-12 col-md-7'p>>"
    });
}
