$(document).ready(function () {

});

function follow(id) {
    if (confirm("Are you sure you want to follow this user?")) {
        var source = '/Profiles/Follow?id=' + id;

        $.ajax({
            url: source,
            error: function (response) {
                location.reload(true);

            },
            dataType: "json",
            contentType: 'application/json',
        });
        
    }
}