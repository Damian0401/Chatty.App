import { Box, Tooltip, Text } from "@chakra-ui/react";
import { formatDistanceToNow } from "date-fns";
import { observer } from "mobx-react-lite"
import { Message } from "../../../app/models/message";
import { useStore } from "../../../app/stores/store";

interface Props {
    message: Message;
}


export default observer(function ChatMessageSystem({ message }: Props) {

    const { userStore: { user } } = useStore();

    return (
        <Box>
            <Tooltip hasArrow label={formatDistanceToNow(message.createdAt) + ' ago'}>
                <Box
                    p='2' m='1'
                    width='100%'
                    bgColor='none'
                    float={message.authorId === user?.id ? 'right' : 'left'}
                    borderRadius='1rem'
                    textAlign='center'
                >
                    <Text>
                        {message.body}
                    </Text>
                </Box>
            </Tooltip>
        </Box>
    )
})