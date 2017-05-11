$("#SpecialMenu").change(function () {
    debugger;
    if (this.checked) {
        $("#divNotes").show();
    }
    else {
        $("#divNotes").hide();
    }
});

$("#cmbEsito").change(function () {
    var esitoVal = $("#cmbEsito").val();
    if (esitoVal == "PRESENTE") {
        $("#divConsensoPrivacy").show();
        $("#divDettagliPresenza").show();
        $("#chkConsensoPrivacy").prop("checked", true);
    }
    else {
        $("#divDettagliPresenza").hide();
        $("#divConsensoPrivacy").hide();
        $("#chkConsensoPrivacy").prop("checked", false);
    }
});