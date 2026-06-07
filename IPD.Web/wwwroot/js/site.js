// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.


function convertDate(dateInp) {
    let date = new Date(dateInp);
    const yy = date.getFullYear()
    const mm = date.getMonth()
    const dd = date.getDate()
    return `${(dd < 10) ? 0 : ''}${dd}-${(mm < 10) ? 0 : ''}${mm + 1}-${yy}`;
}


function convertDateMMddyyyy(dateInp) {
    let date = new Date(dateInp);
    const yy = date.getFullYear()
    const mm = date.getMonth()
    const dd = date.getDate()
    return `${(mm < 10) ? 0 : ''}${mm + 1}/${(dd < 10) ? 0 : ''}${dd}/${yy}`;
}

var baseApi = {
    getRequest: function (url, success, error) {
        let baseurl = $('#baseurl').val();
        const fullUrl = `${baseurl}/${url}`;
        $.ajax({
            type: "GET",
            url: fullUrl,
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (data) {
                success(data)
            },

            failure: function (data) {
                error(data)
            },
            error: function (data) {
                error(data)
            }

        });
    },
    postRequest: function (url, data, success, error) {
        let baseurl = $('#baseurl').val();
        const fullUrl = `${baseurl}/${url}`;
        $.ajax({
            type: "POST",
            url: fullUrl,
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            data: JSON.stringify(data),
            success: function (res) {
                success(res);
            },

            failure: function (res) {
                error(res);
            },
            error: function (res) {
                error(res);
            }

        });
    }
}
