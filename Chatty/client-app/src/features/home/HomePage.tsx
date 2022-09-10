import { Button, Center } from "@chakra-ui/react";


export default function HomePage() {

    return (
        <Center height='100vh'>
            <Button m='2' size='md' variant='main-page'>
                Login
            </Button>
            <Button m='2' size='md' variant='main-page'>
                Register
            </Button>
        </Center>
    )
}