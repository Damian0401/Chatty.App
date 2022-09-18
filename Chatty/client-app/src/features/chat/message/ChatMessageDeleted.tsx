import { Box, Tooltip, Text } from "@chakra-ui/react";
import { formatDistanceToNow } from "date-fns";
import { observer } from "mobx-react-lite";
import { Message } from "../../../app/models/message";
import { useStore } from "../../../app/stores/store";

interface Props {
    message: Message;
}

export default observer(function ChatMessageDeleted({ message }: Props) {

    const { userStore: { user }, chatStore: { selectedRoom } } = useStore();

    return (
        <Box>
            <Tooltip hasArrow label={formatDistanceToNow(message.createdAt) + ' ago'}>
                <Box
                    p='2' m='1'
                    width='fit-content'
                    bgColor='whiteAlpha.300'
                    float={message.authorId === user?.id ? 'right' : 'left'}
                    borderRadius='1rem'
                    border='0.1rem solid'
                    boxShadow='inset 0.04rem 0.02rem 0.05rem'
                    borderColor='whiteAlpha.400'
                    maxWidth='100%'
                >
                    <Text>
                        {message.body}
                    </Text>
                </Box>
            </Tooltip>
        </Box>
    )
})