$(document).ready(function() {
    plotMap();
});

function plotMap() {
    var map = L.map('mapid');

    L.tileLayer('http://{s}.tile.osm.org/{z}/{x}/{y}{r}.png', {
        attribution: '© OpenStreetMap contributors'
    }).addTo(map);

    L.Routing.control({
        waypoints: [
            L.latLng(45.309640, -122.767030),
            L.latLng(45.517560, -122.537820)
        ],
        units: 'imperial',
        routeWhileDragging: true
    }).addTo(map);
}