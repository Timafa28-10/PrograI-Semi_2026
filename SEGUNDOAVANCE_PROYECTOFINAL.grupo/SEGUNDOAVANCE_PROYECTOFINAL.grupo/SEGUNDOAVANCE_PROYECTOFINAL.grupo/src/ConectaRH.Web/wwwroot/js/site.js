// Mostrar / ocultar contraseña (ojo en Plantillas 2 y 3)
document.querySelectorAll('[data-toggle-password]').forEach(function (btn) {
    btn.addEventListener('click', function () {
        var input = document.getElementById(btn.dataset.togglePassword);
        if (!input) return;
        var mostrar = input.type === 'password';
        input.type = mostrar ? 'text' : 'password';
        var icono = btn.querySelector('i');
        if (icono) icono.className = mostrar ? 'bi bi-eye-fill' : 'bi bi-eye-slash-fill';
    });
});

// Plantilla 8: arrastrar o seleccionar el CV
(function () {
    var drop = document.getElementById('po-drop');
    var file = document.getElementById('po-file');
    var nombre = document.getElementById('po-nombre');
    if (!drop || !file) return;

    file.addEventListener('change', function () {
        if (file.files.length && nombre) nombre.textContent = file.files[0].name;
    });
    ['dragenter', 'dragover'].forEach(function (ev) {
        drop.addEventListener(ev, function (e) { e.preventDefault(); drop.classList.add('over'); });
    });
    ['dragleave', 'drop'].forEach(function (ev) {
        drop.addEventListener(ev, function (e) { e.preventDefault(); drop.classList.remove('over'); });
    });
    drop.addEventListener('drop', function (e) {
        if (e.dataTransfer && e.dataTransfer.files.length) {
            file.files = e.dataTransfer.files;
            file.dispatchEvent(new Event('change'));
        }
    });
})();

// Cerrar los menús desplegables (⋮ y usuario) al hacer clic fuera
document.addEventListener('click', function (e) {
    document.querySelectorAll('details.adm-actions[open], details.adm-userbox[open]').forEach(function (d) {
        if (!d.contains(e.target)) d.removeAttribute('open');
    });
});
