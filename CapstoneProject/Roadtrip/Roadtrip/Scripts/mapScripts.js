$(document).ready(function() {
    plotMap();
});

function plotMap() {
    var map = L.map('routemap').setView([44.872435, -123.176427], 13);

    L.tileLayer('http://{s}.tile.osm.org/{z}/{x}/{y}{r}.png', {
        attribution: '© OpenStreetMap contributors'
    }).addTo(map);
    //L.Routing.control({
    //    waypoints: [
    //        L.latLng(57.74, 11.94),
    //        L.latLng(57.6792, 11.949)
    //    ],
    //    router: L.Routing.mapbox('pk.eyJ1IjoibWFwYm94IiwiYSI6ImNpejY4NXVycTA2emYycXBndHRqcmZ3N3gifQ.rJcFIG214AriISLbB6B5aw')
    //}).addTo(map);

}


