var f = document.getElementById('patients');
f.cb_all.onchange = function (e) {
    var el = e.target || e.srcElement;
    var qwe = el.form.getElementsByClassName('patient');
    for (var i = 0; i < qwe.length; i++) {
        if (el.checked) {
            qwe[i].checked = true;
        } else {
            qwe[i].checked = false;
        }
    }
}