$(function() {
	/* Acivate Appropriate Nav Items */
	$('#sidebar .nav li.active').removeClass('active');
	var pathname = window.location.pathname.split('/');
	var filename = pathname[pathname.length-1];
	$('#sidebar .nav li:has(a[href="' + filename + '"])').addClass('active').parent('li').addClass('active');
});