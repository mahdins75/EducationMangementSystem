function openInNewTab(url) {
    if (url) {
        window.open(url, '_blank');
    } else {
        console.error('URL is not provided or invalid.');
    }
}