$(document).ready(function () {

	/*
	 * Trigger für Icon-Click
	 */
	$('#nav-icon').click(function () {
		let nav = $('#main-nav');
		nav.css('height', $('#main-wrapper').height());

		$(this).toggleClass('open');

		if (nav.hasClass('open')) {
			nav.removeClass('open');
			nav.addClass('close');
			setTimeout(function () {
				nav.removeClass('close');
			}, 250);
		} else {
			nav.removeClass('close');
			nav.addClass('open');
        }
	});
});