function setupAutocomplete(inputElement, resultsElement, hiddenInput) {
    let timeoutId;

    inputElement.addEventListener('input', function(e) {
        clearTimeout(timeoutId);
        const searchText = e.target.value;

        if (searchText.length < 2) {
            resultsElement.innerHTML = '';
            return;
        }

        timeoutId = setTimeout(async () => {
            try {
                const response = await fetch(`/Home/GetBusLocations?searchText=${encodeURIComponent(searchText)}`);
                const locations = await response.json();

                resultsElement.innerHTML = '';
                locations.forEach(location => {
                    const div = document.createElement('div');
                    div.className = 'autocomplete-item';
                    div.textContent = location.name;
                    div.addEventListener('click', () => {
                        inputElement.value = location.name;
                        hiddenInput.value = location.id;
                        resultsElement.innerHTML = '';
                    });
                    resultsElement.appendChild(div);
                });
            } catch (error) {
                console.error('ERROR:', error);
            }
        }, 300);
    });
    
    document.addEventListener('click', function(e) {
        if (!resultsElement.contains(e.target) && e.target !== inputElement) {
            resultsElement.innerHTML = '';
        }
    });
}

document.getElementById('swapLocations')?.addEventListener('click', function() {
    const originInput = document.getElementById('origin');
    const originHidden = document.getElementById('originId');
    const destinationInput = document.getElementById('destination');
    const destinationHidden = document.getElementById('destinationId');

    [originInput.value, destinationInput.value] = [destinationInput.value, originInput.value];
    [originHidden.value, destinationHidden.value] = [destinationHidden.value, originHidden.value];
});

document.querySelectorAll('.date-btn').forEach(btn => {
    btn.addEventListener('click', function() {
        const days = parseInt(this.dataset.days);
        const date = new Date();
        date.setDate(date.getDate() + days);

        document.getElementById('departureDate').value = date.toISOString().split('T')[0];
    });
});

document.getElementById('searchForm')?.addEventListener('submit', function(e) {
    e.preventDefault();

    const originId = document.getElementById('originId').value;
    const destinationId = document.getElementById('destinationId').value;
    const departureDate = document.getElementById('departureDate').value;

    if (!originId || !destinationId) {
        alert('Kalkış ve varış noktalarını seçiniz.');
        return;
    }

    if (originId === destinationId) {
        alert('Kalkış ve varış noktaları aynı olamaz.');
        return;
    }

    fetch('/Home/SetLastSearch', {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json',
        },
        body: JSON.stringify({
            OriginId: parseInt(originId),
            DestinationId: parseInt(destinationId),
            DepartureDate: departureDate
        })
    });

    window.location.href = `/Journey/Index?originId=${originId}&destinationId=${destinationId}&departureDate=${departureDate}`;
});

document.addEventListener('DOMContentLoaded', function() {
    const originInput = document.getElementById('origin');
    const originResults = document.getElementById('originResults');
    const originHidden = document.getElementById('originId');

    const destinationInput = document.getElementById('destination');
    const destinationResults = document.getElementById('destinationResults');
    const destinationHidden = document.getElementById('destinationId');

    if (originInput) {
        setupAutocomplete(originInput, originResults, originHidden);
    }

    if (destinationInput) {
        setupAutocomplete(destinationInput, destinationResults, destinationHidden);
    }
});