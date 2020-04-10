function calculateValues() {
    let dayAmount = parseFloat($("#inputDayChargeRate").val()) * parseFloat($("#inputDayChargeUnits").val());
    let nightAmount = parseFloat($("#inputNightChargeRate").val()) * parseFloat($("#inputNightChargeUnits").val());
    let weekendAmount = parseFloat($("#inputWeekendChargeRate").val()) * parseFloat($("#inputWeekendChargeUnits").val());

    let dayTax = (dayAmount * 0.17).toFixed(2);
    let nightTax = (nightAmount * 0.17).toFixed(2);
    let weekendTax = (weekendAmount * 0.17).toFixed(2);

    let total = parseFloat(dayAmount + nightAmount + weekendAmount + dayTax + nightTax + weekendTax);

    $("#inputDayChargeAmount").val(dayAmount.toFixed(1));
    $("#inputNightChargeAmount").val(nightAmount.toFixed(1));
    $("#inputWeekendChargeAmount").val(weekendAmount.toFixed(1));

    $("#inputDayChargeTax").val(dayTax);
    $("#inputNightChargeTax").val(nightTax);
    $("#inputWeekendChargeTax").val(weekendTax);

    $("#inputChargeTotal").val(total.toFixed(2));
}