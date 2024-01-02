/** @type {import('tailwindcss').Config} */
export const content = ["./src/**/*.{html,ts}"];
export const theme = {
  extend: {},
  colors: {
    transparent: "transparent",
    primary: "#213140",
    secondary: "#a9be55",
    white: "#EEE",
    light: "#FAEEC3",
    gray: {
      50: "#9D8B73",
    },
  },
  screens: {
    sm: "300px",
    md: "768px",
    lg: "1024px",
    xl: "1280px",
    "2xl": "1536px",
  },
};
export const plugins = [];
