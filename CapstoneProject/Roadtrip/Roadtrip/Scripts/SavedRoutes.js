$(document).ready(function () {

    if (RouteList.length > 0)
        $('#routeCol').empty();

    for (var i = 0; i < RouteList.length; i++) {

        
        $('#routeCol').append(`
<div style="display:table; width:100%">
        <div style="display: table-row">
            <div style="width: 100%; display: table-cell; background-color:antiquewhite; border:1px solid black;">
                <div  style="font-size: 15px;">Start: ${RouteList[i].Locations[0].Name}</div>
                <div  style="font-size: 15px;">End: ${RouteList[i].Locations[RouteList[i].Locations.length - 1].Name}</div>
                <p style="font-size: 10px;">Created on ${ moment(RouteList[i].Timestamp).format('MMMM Do YYYY, h:mm a')}</p>
                <input id="${i}" type="button" value="Show Route" onclick="showRoute(this.id)">
            </div>
        </div>
</div>
`);
    }
});

function showRoute(id) {

    document.getElementById('routemap').innerHTML = "<div id='rmap' style='width: 100%; height:80vh;'></div>";
    var mymap = L.map('rmap').setView([45, -123], 13);

    L.tileLayer('https://api.mapbox.com/styles/v1/{id}/tiles/{z}/{x}/{y}?access_token=pk.eyJ1IjoibWFwYm94IiwiYSI6ImNpejY4NXVycTA2emYycXBndHRqcmZ3N3gifQ.rJcFIG214AriISLbB6B5aw', {
        maxZoom: 18,
        attribution: 'Map data &copy; <a href="https://www.openstreetmap.org/">OpenStreetMap</a> contributors, ' +
            '<a href="https://creativecommons.org/licenses/by-sa/2.0/">CC-BY-SA</a>, ' +
            'Imagery © <a href="https://www.mapbox.com/">Mapbox</a>',
        id: 'mapbox/streets-v11',
        tileSize: 512,
        zoomOffset: -1
    }).addTo(mymap);

    var route = [];
    routewps = [];

    for (var i = 0; i < RouteList[id].Locations.length; i++) {
        routewps.push(L.latLng([RouteList[id].Locations[i].Latitude, RouteList[id].Locations[i].Longitude]));
    }

    var control = L.Routing.control({
        waypoints: routewps,
        units: 'imperial',
        router: L.Routing.mapbox('pk.eyJ1IjoibWFwYm94IiwiYSI6ImNpejY4NXVycTA2emYycXBndHRqcmZ3N3gifQ.rJcFIG214AriISLbB6B5aw')
    }).addTo(mymap);
    control.hide();

    for (var i = 0; i < RouteList[id].Locations.length; i++) {
        route.push(L.marker([RouteList[id].Locations[i].Latitude, RouteList[id].Locations[i].Longitude]).bindPopup("<b>" + RouteList[id].Locations[i].Name + "</b>").addTo(mymap));
    }

    var group = new L.featureGroup(route);

    mymap.fitBounds(group.getBounds());
}
