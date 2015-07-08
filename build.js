!function(e){if("object"==typeof exports&&"undefined"!=typeof module)module.exports=e();else if("function"==typeof define&&define.amd)define([],e);else{var t;t="undefined"!=typeof window?window:"undefined"!=typeof global?global:"undefined"!=typeof self?self:this,t.BezierEasing=e()}}(function(){return function e(t,n,r){function i(s,u){if(!n[s]){if(!t[s]){var f="function"==typeof require&&require;if(!u&&f)return f(s,!0);if(o)return o(s,!0);var a=new Error("Cannot find module '"+s+"'");throw a.code="MODULE_NOT_FOUND",a}var p=n[s]={exports:{}};t[s][0].call(p.exports,function(e){var n=t[s][1][e];return i(n?n:e)},p,p.exports,e,t,n,r)}return n[s].exports}for(var o="function"==typeof require&&require,s=0;s<r.length;s++)i(r[s]);return i}({1:[function(e,t,n){(function(e){function n(e,t){return 1-3*t+3*e}function r(e,t){return 3*t-6*e}function i(e){return 3*e}function o(e,t,o){return((n(t,o)*e+r(t,o))*e+i(t))*e}function s(e,t,o){return 3*n(t,o)*e*e+2*r(t,o)*e+i(t)}function u(e,t,n,r,i){var s,u,f=0;do u=t+(n-t)/2,s=o(u,r,i)-e,s>0?n=u:t=u;while(Math.abs(s)>l&&++f<h);return u}function f(e,t,n,r){for(var i=0;p>i;++i){var u=s(t,n,r);if(0===u)return t;var f=o(t,n,r)-e;t-=f/u}return t}function a(e,t,n,r){if(4===arguments.length)return new a([e,t,n,r]);if(!(this instanceof a))return new a(e);if(!e||4!==e.length)throw new Error("BezierEasing: points must contains 4 values");for(var i=0;4>i;++i)if("number"!=typeof e[i]||isNaN(e[i])||!isFinite(e[i]))throw new Error("BezierEasing: points should be integers.");if(e[0]<0||e[0]>1||e[2]<0||e[2]>1)throw new Error("BezierEasing x values must be in [0, 1] range.");this._str="BezierEasing("+e+")",this._css="cubic-bezier("+e+")",this._p=e,this._mSampleValues=m?new Float32Array(d):new Array(d),this._precomputed=!1}var p=4,c=.001,l=1e-7,h=10,d=11,_=1/(d-1),m="Float32Array"in e;a.prototype={get:function(e){var t=this._p[0],n=this._p[1],r=this._p[2],i=this._p[3];return this._precomputed||this._precompute(),t===n&&r===i?e:0===e?0:1===e?1:o(this._getTForX(e),n,i)},getPoints:function(){return this._p},toString:function(){return this._str},toCSS:function(){return this._css},_precompute:function(){var e=this._p[0],t=this._p[1],n=this._p[2],r=this._p[3];this._precomputed=!0,(e!==t||n!==r)&&this._calcSampleValues()},_calcSampleValues:function(){for(var e=this._p[0],t=this._p[2],n=0;d>n;++n)this._mSampleValues[n]=o(n*_,e,t)},_getTForX:function(e){for(var t=this._p[0],n=this._p[2],r=this._mSampleValues,i=0,o=1,a=d-1;o!==a&&r[o]<=e;++o)i+=_;--o;var p=(e-r[o])/(r[o+1]-r[o]),l=i+p*_,h=s(l,t,n);return h>=c?f(e,l,t,n):0===h?l:u(e,i,i+_,t,n)}},a.css={ease:a.ease=a(.25,.1,.25,1),linear:a.linear=a(0,0,1,1),"ease-in":a.easeIn=a(.42,0,1,1),"ease-out":a.easeOut=a(0,0,.58,1),"ease-in-out":a.easeInOut=a(.42,0,.58,1)},t.exports=a}).call(this,"undefined"!=typeof global?global:"undefined"!=typeof self?self:"undefined"!=typeof window?window:{})},{}]},{},[1])(1)});