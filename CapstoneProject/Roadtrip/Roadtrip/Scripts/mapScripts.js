$(document).ready(function() {
    plotMap();
});

function plotMap() {
    var mymap = L.map('mapid').setView([47.622370, -122.347020], 13);
    L.tileLayer('https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png', {
        attribution: '&copy; <a href="https://www.openstreetmap.org/copyright">OpenStreetMap</a> contributors'
    }).addTo(mymap);
    var greenIcon = L.icon({
        iconUrl: 'https://cdn4.iconfinder.com/data/icons/iconsimple-places/512/pin_1-512.png',
        iconSize: [40, 40], // size of the icon
        shadowSize: [20, 20], // size of the shadow
        iconAnchor: [20, 40], // point of the icon which will correspond to marker's location
        shadowAnchor: [20, 40],  // the same for the shadow
        popupAnchor: [0, -40] // point from which the popup should open relative to the iconAnchor
    });

    L.marker([47.622370, -122.347020], { icon: greenIcon }).addTo(mymap).bindPopup("Seattle");
}