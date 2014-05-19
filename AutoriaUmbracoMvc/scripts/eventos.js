
$(document).ready(function () {

    var json = $.parseJSON($('#eventos').val());
    json.pop(json.length-1);

    $("#eventCalendarOnlyOneDescription").eventCalendar({
        jsonData: json,
        moveSpeed: 600,
        openEventInNewWindow: true,
        showDescription: true,
        monthNames: ["Janeiro", "Fevereiro", "Março", "Abril", "Maio", "Junho", "Julho", "Agosto", "Setembro", "Outrubro", "Novembro", "Dezembro"],
        dayNames: ['Domingo', 'Segunda', 'Terça', 'Quarta', 'Quinta', 'Sexta', 'Sábado'],
        dayNamesShort: ["Dom", "Seg", "Ter", "Qua", "Qui", "Sex", "Sab"],
        txt_noEvents: "Nenhum evento",
        txt_SpecificEvents_prev: "",
        txt_SpecificEvents_after: "eventos:",
        txt_next: "próximo",
        txt_prev: "anterior",
        txt_NextEvents: "Próximos eventos:",
        txt_GoToEventUrl: "Ir à pagina do evento",
        moveOpacity: 0,
        jsonDateFormat: 'human'
    });

    $(".horarios-aula").appendTo('#eventCalendarOnlyOneDescription');

});
