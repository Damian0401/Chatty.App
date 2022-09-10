import { extendTheme, StyleFunctionProps } from "@chakra-ui/react";
import { mode } from "@chakra-ui/theme-tools";

const lightBackground = 'linear-gradient(90deg, rgba(40,31,133,1) 0%, rgba(69,41,213,0.9351890414368873) 36%, rgba(149,63,140,0.9435924027814251) 100%)';
const darkBackground = 'linear-gradient(90deg, rgba(20,16,61,1) 0%, rgba(37,22,113,0.9351890414368873) 36%, rgba(91,19,84,0.9435924027814251) 100%)';

const theme = extendTheme({
    styles: {
        global: (props: StyleFunctionProps) => ({
            body: {
                bg: mode(lightBackground, darkBackground)(props),
                fontFamily: 'Sans-Serif'
            },
        })
    },
    components: {
        Button: {
            variants: {
                'main-page': {
                    bgColor: 'whiteAlpha.300',
                    boxShadow: '8px 3px 12px -2px rgba(66, 68, 90, 1)',
                    _hover: {
                        bgColor: 'whiteAlpha.600'
                    }
                }
            }
        }
    }
});

export default theme;