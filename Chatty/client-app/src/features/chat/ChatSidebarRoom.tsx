import { Box, Flex, Spacer, Text } from "@chakra-ui/react";
import { observer } from "mobx-react-lite";
import { Link } from "react-router-dom";


export default observer(function ChatSidebarRoom() {



    return (
        <Box bgColor='whiteAlpha.200' p='1' maxWidth='12vw' as={Link} to='/chat/5' borderRadius='0.2rem'>
            <Flex>
                <Text noOfLines={1} as='b'>
                    ChannelName:
                </Text>
                <Spacer />
            </Flex>
            <Text noOfLines={1}>UserName: This is very long last message</Text>
        </Box>
    )
})