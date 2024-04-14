document.getElementById('yourDivId').addEventListener('mousemove', function (event) {
    var rect = this.getBoundingClientRect();
    var x = event.clientX - rect.left; // Coordonnée X relative à la div
    var y = event.clientY - rect.top; // Coordonnée Y relative à la div
    console.log(`X: ${x}, Y: ${y}`); // Affiche les coordonnées relatives
});