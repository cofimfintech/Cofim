// ****************************************** GENERIC FUNCTION INI ******************************************

function formatMontoMinimo(mm) {
    if (mm == null) {
        document.getElementById('MontoMinimo').ej2_instances[0].value = 100000;
        return 100000
    }
    return mm
}
function simpleStringify(object) {
    var simpleObject = {};
    for (var prop in object) {
        if (!object.hasOwnProperty(prop)) { continue; }
        if (typeof (object[prop]) == 'object') { simpleObject[prop] = object[prop].value; continue; }
        if (typeof (object[prop]) == 'function') { continue; }
        simpleObject[prop] = object[prop];
    }
    return JSON.stringify(simpleObject); // returns cleaned up JSON
};

function formatDate(date) {

    var d = (date === '' || date == null ? Date.now() : new Date(date)),
        month = '' + (d.getMonth() + 1),
        day = '' + d.getDate(),
        year = d.getFullYear();

    if (month.length < 2)
        month = '0' + month;
    if (day.length < 2)
        day = '0' + day;

    return [year, month, day].join('-');
}

function getToday() {
    var today = new Date(Date.now());
    var dd = String(today.getDate()).padStart(2, '0');
    var mm = String(today.getMonth() + 1).padStart(2, '0'); //January is 0!
    var yyyy = today.getFullYear();

    return today = mm + '/' + dd + '/' + yyyy;
}



                            // ****************************************** GENERIC FUNCTION END ******************************************