const popupButton = document.getElementById('review-popup-button')
const popupCloseButton = document.getElementById('close-review-popup');
const popupName = popupButton.getAttribute('href').replace('#', '');
const timeout = 800;

popupButton.addEventListener("click", function (e)
{
    const contentName = document.getElementById('review-popup-content');
    const currentPopup = document.getElementById(popupName);
    popupOpen(currentPopup, contentName);
    e.preventDefault();
})

popupCloseButton.addEventListener("click", function (e)
{
    const contentName = document.getElementById('review-popup-content');
    const currentPopup = document.getElementById(popupName);
    popupClose(currentPopup, contentName);
    e.preventDefault();
})


function popupOpen(currentPopup, contentName)
{
    currentPopup.classList.add('open');
    currentPopup.classList.remove('closed');


    if (contentName.classList.contains('closed'))
    {
        contentName.classList.remove('closed');
    }
}

function popupClose(currentPopup, contentName)
{
    currentPopup.classList.remove('open');
    currentPopup.classList.add('closed');


    contentName.classList.add('closed');
}

jQuery(document).mouseup(function (e) {
    var container = document.getElementById('review-popup');
    var content = document.getElementById('review-popup-content');
    if (container.classList.contains('open'))
    {
        if (!$(e.target).closest(".review-popup").length) {
            popupClose(container,content)
        }
    }
})

jQuery(document).ready(function ()
{
    $('.rating').each(function ()
    {
        var cellValue = $(this).html();
        if (!isNaN(parseFloat(cellValue))) {
            if (cellValue < 5) {
                $(this).css('color', 'red');
            }
            else if (cellValue < 7) {
                $(this).css('color', 'grey');
            }
            else {
                $(this).css('color', 'green');
            }
        }
    });
});
