function toFixed(number, digits) {
    return number.toFixed(digits);
}
function eq(v1, v2) {
    return v1 === v2;
}
function ne(v1, v2) {
    return v1 !== v2;
}
function lt(v1, v2) {
    return v1 < v2;
}
function gt(v1, v2) {
    return v1 > v2;
}
function lte(v1, v2) {
    return v1 <= v2;
}
function gte(v1, v2) {
    return v1 >= v2;
}
function and() {
    return Array.prototype.slice.call(arguments).every(Boolean);
}
function or() {
    return Array.prototype.slice.call(arguments, 0, -1).some(Boolean);
}
