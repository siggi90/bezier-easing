using System;

using Xamarin.Forms;
using System.Collections.Generic;

namespace CubicBezierEasings {
    public class easings {
        public Easing teal;

        double NEWTON_ITERATIONS = 4;
        double NEWTON_MIN_SLOPE = 0.001;
        double SUBDIVISION_PRECISION = 0.0000001;
        double SUBDIVISION_MAX_ITERATIONS = 10;

        double kSplineTableSize = 11;
        double kSampleStepSize; 


        public easings() {
            this.kSampleStepSize = 1.0 / (this.kSplineTableSize - 1.0);

        }

        double A (double aA1, double aA2) { return 1.0 - 3.0 * aA2 + 3.0 * aA1; }
        double B (double aA1, double aA2) { return 3.0 * aA2 - 6.0 * aA1; }
        double C (double aA1) { return 3.0 * aA1; }
        double calcBezier (double aT, double aA1, double aA2) { return ((A(aA1, aA2) * aT + B(aA1, aA2)) * aT + C(aA1)) * aT; }
        double getSlope (double aT, double aA1, double aA2) { return 3.0 * A(aA1, aA2) * aT * aT + 2.0 * B(aA1, aA2) * aT + C(aA1); }

        double binarySubdivide (double aX, double aA, double aB, double mX1, double mX2) {
            double currentX, currentT, i = 0;
            do {
                currentT = aA + (aB - aA) / 2.0;
                currentX = calcBezier(currentT, mX1, mX2) - aX;
                if (currentX > 0.0) {
                    aB = currentT;
                } else {
                    aA = currentT;
                }
            } while (Math.Abs(currentX) > SUBDIVISION_PRECISION && ++i < SUBDIVISION_MAX_ITERATIONS);
            return currentT;
        }

        double newtonRaphsonIterate (double aX, double aGuessT, double mX1, double mX2) {
            for (var i = 0; i < NEWTON_ITERATIONS; ++i) {
                var currentSlope = getSlope(aGuessT, mX1, mX2);
                if (currentSlope == 0.0) {
                    return aGuessT;
                }
                var currentX = calcBezier(aGuessT, mX1, mX2) - aX;
                aGuessT -= currentX / currentSlope;
            }
            return aGuessT;
        }
                
        double LinearEasing (double x) {
            return x;
        }

        public Easing bezier (double mX1, double mY1, double mX2, double mY2) {
            if (!(0 <= mX1 && mX1 <= 1 && 0 <= mX2 && mX2 <= 1)) {
            //throw new Error('bezier x values must be in [0, 1] range');
            }

            if (mX1 == mY1 && mX2 == mY2) {
            //return LinearEasing;
            }

            // Precompute samples table
            var sampleValues = new List<double>();
            for (var i = 0; i < kSplineTableSize; ++i) {
                sampleValues.Add(calcBezier(i * kSampleStepSize, mX1, mX2));
            }

           

            /*return function BezierEasing (x) {
            // Because JavaScript number are imprecise, we should guarantee the extremes are right.
            if (x === 0 || x === 1) {
                return x;
            }
            return calcBezier(getTForX(x), mY1, mY2);
            };*/
            return new Easing(x => {
                double result = this.calcBezier(this.getTForX(x, sampleValues, mX1, mY1, mX2, mY2), mY1, mY2);
                return result;
            });
        }

         double getTForX (double aX, List<double> sampleValues, double mX1, double mY1, double mX2, double mY2) {
            var intervalStart = 0.0;
            var currentSample = 1;
            var lastSample = kSplineTableSize - 1;

            for (; currentSample != lastSample && sampleValues[currentSample] <= aX; ++currentSample) {
                intervalStart += kSampleStepSize;
            }
            --currentSample;

            // Interpolate to provide an initial guess for t
            var dist = (aX - sampleValues[currentSample]) / (sampleValues[currentSample + 1] - sampleValues[currentSample]);
            var guessForT = intervalStart + dist * kSampleStepSize;

            var initialSlope = getSlope(guessForT, mX1, mX2);
            if (initialSlope >= NEWTON_MIN_SLOPE) {
                return newtonRaphsonIterate(aX, guessForT, mX1, mX2);
            } else if (initialSlope == 0.0) {
                return guessForT;
            } else {
                return binarySubdivide(aX, intervalStart, intervalStart + kSampleStepSize, mX1, mX2);
            }
         }
    }
}
