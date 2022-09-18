import { Button, Center, Container, Flex, FormControl, FormLabel, Input, Text } from "@chakra-ui/react"
import { observer } from "mobx-react-lite"
import { useStore } from "../../../app/stores/store";



export default observer(function ChatPanel() {

    const { userStore: { user } } = useStore();

    return (
        <Center>
            <Container
                p='8' m='3'
                variant='main-style'
                maxWidth='40vw'
                height='70vh'
            >
                <Flex flexDirection='column' height='100%' justifyContent='space-between'>
                    <Text fontSize='2xl' noOfLines={3}>
                        Welcome to the chat control panel, <strong>{user ? user.userName : '{userName}'}</strong>.
                        Here you can create a new chat room or join to existing one.
                    </Text>
                    <Flex justifyContent='space-between'>
                        <Button variant='main-style' size='lg' onClick={() => console.log('Open join modal')}>
                            Join
                        </Button>
                        <Button variant='main-style' size='lg' onClick={() => console.log('Open create modal')}>
                            Create
                        </Button>
                    </Flex>
                </Flex>
            </Container>
        </Center>
    )
})