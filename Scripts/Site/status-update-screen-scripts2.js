/* Status Update Screen Specific Scrpits */
$(function() {
    // Init Calculate Beds
    $('.calculate-beds-row input.beds-in-use').each(function() {
        var row = $(this).parents('.calculate-beds-row');
        var currentCapacity = row.find('.current-capacity').val();
        var bedsInUse = row.find('.beds-in-use').val();
        calculateBeds(row, currentCapacity, bedsInUse);
    });

    // Calculate Beds on Change
    $('.calculate-beds-row input.current-capacity, .calculate-beds-row input.beds-in-use').change(function() {
        var row = $(this).parents('.calculate-beds-row');
        var currentCapacity = row.find('.current-capacity').val();
        var bedsInUse = row.find('.beds-in-use').val();
        calculateBeds(row, currentCapacity, bedsInUse);
    });

    // Init Calculate Staff
    $('.calculate-staff-row input.staff-reported').each(function() {
        var row = $(this).parents('.calculate-staff-row');
        var scheduled = row.find('.staff-scheduled').val();
        var reported = row.find('.staff-reported').val();
        calculateStaff(row, scheduled, reported);
    });

    // Calculate Staff on Change
    $('.calculate-staff-row input.staff-scheduled, .calculate-staff-row input.staff-reported').change(function() {
        var row = $(this).parents('.calculate-staff-row');
        var scheduled = row.find('.staff-scheduled').val();
        var reported = row.find('.staff-reported').val();
        calculateStaff(row, scheduled, reported);
    });

    // Init System Status
    $('.system-status-row .btn-group').each(function() {
        var row = $(this).parents('.system-status-row');
        var selectedButton = $(this).find('.selected');
        systemStatusRow(row, selectedButton);
    });

    // Calculate System Status on Change
    $('.system-status-row button').click(function() {
        var row = $(this).parents('.system-status-row');
        var selectedButton = $(this);
        systemStatusRow(row, selectedButton);
    });

    // Init Calculate Fuel
    $('.calculate-fuel-row input.amount-needed').each(function() {
        var row = $(this).parents('.calculate-fuel-row');
        var amountNeeded = row.find('.amount-needed').val();
        calculateFuel(row, amountNeeded);
    });

    // Calculate Fuel on Change
    $('.calculate-fuel-row input.amount-needed').change(function() {
        var row = $(this).parents('.calculate-fuel-row');
        var amountNeeded = row.find('.amount-needed').val();
        calculateFuel(row, amountNeeded);
    });
});

function calculateBeds(row, currentCapacity, bedsInUse) {
    var numberAvailable = currentCapacity - bedsInUse;
    var outputArea = row.find('.number-available');
    var outputIcon = row.find('i.fa');
    // Calculate Bed Availability
    if (numberAvailable <= 0) {
        outputArea.html('0');
        outputIcon.attr('class', 'fa fa-square red');
        row.attr('class', 'form-group border-left border-red calculate-beds-row');
    } else if ( numberAvailable <= (currentCapacity*0.1) && numberAvailable > 0 ) {
        outputArea.html(numberAvailable);
        outputIcon.attr('class', 'fa fa-square yellow');
        row.attr('class', 'form-group border-left border-yellow calculate-beds-row');
    } else {
        outputArea.html(numberAvailable);
        outputIcon.attr('class', 'fa fa-square green');
        row.attr('class', 'form-group border-left border-green calculate-beds-row');
    }
}

function calculateStaff(row, scheduled, reported) {
    var bedsPerStaff = row.attr('data-beds-per-staff');
    var bedsStaffedOutputArea = row.find('.display-beds-covered .number');
    var numberShort = scheduled - reported;
    var numberShortOutputArea = row.find('.number-short');
    var outputIcon = row.find('i.fa');
    // Calculate Staff Shortage
    if (numberShort <= 0) {
        numberShortOutputArea.html('0');
        outputIcon.attr('class', 'fa fa-square green');
        row.attr('class', 'form-group border-left border-green calculate-staff-row');
    } else if ( numberShort <= (scheduled*0.1) && numberShort > 0 ) {
        numberShortOutputArea.html(numberShort);
        outputIcon.attr('class', 'fa fa-square yellow');
        row.attr('class', 'form-group border-left border-yellow calculate-staff-row');
    } else {
        numberShortOutputArea.html(numberShort);
        outputIcon.attr('class', 'fa fa-square red');
        row.attr('class', 'form-group border-left border-red calculate-staff-row');
    }
    // Calculate Potential Number of Beds Staffed
    var bedsStaffed = reported * bedsPerStaff;
    bedsStaffedOutputArea.html(bedsStaffed);
}

function systemStatusRow(row, selectedButton) {
    if (selectedButton.hasClass('btn-success')) {
        row.attr('class', 'form-group system-status-row border-left border-green');
    } else if (selectedButton.hasClass('btn-warning')) {
        row.attr('class', 'form-group system-status-row border-left border-yellow');
    } else if (selectedButton.hasClass('btn-danger')) {
        row.attr('class', 'form-group system-status-row border-left border-red');
    } else {
        row.attr('class', 'form-group system-status-row border-left');
    }
}

function calculateFuel(row, amountNeeded) {
    var capacity = row.attr('data-fuel-capacity');
    var percentageNeeded = Math.floor((amountNeeded / capacity) * 100); /* Calculation assumes unit measurement is the same */
    var measurement = row.find('.bootstrap-select button.selectpicker').attr('title');
    var outputArea = row.find('.fuel-needed');
    var outputIcon = row.find('i.fa');
    if (amountNeeded <= 0) {
        outputArea.html('Reserves Adequate');
        outputIcon.attr('class', 'fa fa-square green');
        row.attr('class', 'form-group border-left border-green calculate-fuel-row');
    } else if ( percentageNeeded <= 80 && amountNeeded > 0 ) {
        outputArea.html(amountNeeded + ' ' + measurement + ' Needed');
        outputIcon.attr('class', 'fa fa-square yellow');
        row.attr('class', 'form-group border-left border-yellow calculate-fuel-row');
    } else {
        outputArea.html(amountNeeded + ' ' + measurement + ' Needed');
        outputIcon.attr('class', 'fa fa-square red');
        row.attr('class', 'form-group border-left border-red calculate-fuel-row');
    }
}