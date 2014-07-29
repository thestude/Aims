$(function() {
	/* Initialize tooltips */
	$('.title-col .facility-title').tooltip();
	$(".label[data-toggle='tooltip']").tooltip();
	
	/* Initialize height equalization */
	equalHeight($(".grid-cols > [class^='col-']:not(.exclude-from-grid)"));

	/* Initialize Status Btn behavior */
	$('.status-btns button').click(function() {
		var btnClicked = $(this);
		var btnGroup = $(this).parents('.status-btns').find('button');
		var btnInput = $(this).parents('.status-btns').next('input[type="hidden"]');
		statusBtnSelect(btnClicked, btnGroup, btnInput);
	});

	/* Initialize bootstrap select */
	$('.bs-select').selectpicker();

	/* Initialize SlimScroll */
	$('.slimScroll500').slimScroll({
		height: '500px'
	});

	$('.map-legend .nav-tabs a[data-toggle="tab"]').click(function() {
		var arrow = $(this).parents('ul.nav-tabs').find('a[data-toggle="collapse"]');
		var tabContent = $(this).parents('ul.nav-tabs').siblings('.tab-content');
		if (tabContent.hasClass('in')) {
			// do nothing
		} else {
			arrow.attr('class', 'collapsed');
			arrow.find('i.fa').attr('class', 'fa fa-chevron-down');
			tabContent.addClass('in');
		}
	});
});

/* On Window Resize */
$(window).resize(function() {
	equalHeight($(".grid-cols > [class^='col-']:not(.exclude-from-grid)"));
});

/* Equalize heights */
function equalHeight(group) {
	group.css('height', 'auto');
  tallest = 0;
  group.each(function() {
    thisHeight = $(this).height();
    if(thisHeight > tallest) {
      tallest = thisHeight;
    }
  });
  group.height(tallest);
}

/* Status btn click function */
function statusBtnSelect(btn, group, hiddenInput) {
	group.removeClass('selected');
	btn.addClass('selected');
	var btnValue = $(btn).text();
	hiddenInput.val(btnValue);
}

// Javascript to enable link to tab
var url = document.location.toString();
if (url.match('#')) {
    $('.nav-tabs a[href=#'+url.split('#')[1]+']').tab('show') ;
} 

// Change hash for page-reload
$('.nav-tabs a').on('shown', function (e) {
    window.location.hash = e.target.hash;
})

if (jQuery().dataTable) {
    $('.dataTable').dataTable( {
        "aoColumnDefs": [
          { "bSortable": false, "aTargets": [ 0, 1 ] }
        ] 
    } );
}