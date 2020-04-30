

// Show a center notify
function showCenterNotify(message) {
    F.notify({
        message: message,
        messageIcon: '',
        modal: true,
        hideOnMaskClick: true,
        header: false,
        displayMilliseconds: 3000,
        positionX: 'center',
        positionY: 'center',
        messageAlign: 'center',
        minWidth: 200
    });
}
