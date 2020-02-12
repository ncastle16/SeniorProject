function getInfo(Info) {
    $.ajax({
        type: "GET", 
        dataType: "json", 
        url: "/CreateRoute/DisplayInfo",
        data: { 'Info': Info }, 
        success: showInfo, 
        error: errorOnAjax
    })
}
function errorOnAjax() {
    console.log("ERROR in ajax request.");
}

function showInfo(data) {
    $('#outputTable').append($('<table id=\"output\">'));
    $('#output').append($('<tr id=\"tableTr\">'));
    $('#tableTr').append($('<th><strong><center> Times </th>'));
    $('#tableTr').append($('<th><strong><center> Race Types </th>'));
    $('#output').append($('</tr>'));
    $('#outputTable').append($('</table>'));

}