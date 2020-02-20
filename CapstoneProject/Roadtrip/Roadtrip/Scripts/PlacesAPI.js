$(document).ready(function () {
    var source = '/api/establishments';

    $.ajax({
        type: 'GET',
        datatype: 'json',
        url: '/Routes/GetEstablishment',
        success: test,
        error: errorOnAjax
    });
});

function errorOnAjax(data) {
    console.log('Error on AJAX return');
    console.log(data);
}

function test(data) {
    console.log('yay');
    console.log(data);
    for (var i = 0; i < 20; i++) {
        $('#establishments').append(`
<div style="display:table; width:100%">
        <div style="display: table-row">
            <div style="width: 400px; display: table-cell; background-color:antiquewhite; border:1px solid black">
                <ul>${data.name[i]}</ul> 
            </div>
        </div>
</div>
`)
    }
}