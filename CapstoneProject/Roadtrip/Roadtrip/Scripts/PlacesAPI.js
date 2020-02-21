$(document).ready(function () {

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
                <input id="${data.id[i]}" type="button" value="Get Details" onclick="details(this.id)">
            </div>
        </div>
</div>
`);
 }
}

function details(id) {
    var source = '/Routes/GetDetails?id=' + id;

    $.ajax({
        type: 'GET',
        dataType: 'json',
        url: source,
        success: showDetails,
        error: errorOnAjax
    });
}

function showDetails(data) {
    console.log(data);

    $('#details').empty();
    $('#details').append(`<div style="margin-top:50px;margin-bottom:50px;"><img src="' + image + '" style="width:200px;height:150px;"><br> <b>' + name + '</b><br>This business has a rating of ' + rating`);
}