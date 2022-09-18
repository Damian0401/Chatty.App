import { Box, Text, Tooltip } from "@chakra-ui/react";
import { formatDistanceToNow } from "date-fns";
import { observer } from "mobx-react-lite";
import { useEffect, useState } from "react";
import { Message } from "../../../app/models/message";
import { useStore } from "../../../app/stores/store";

interface Props {
    message: Message;
}

export default observer(function ChatRoomMessage({ message }: Props) {

    const { userStore: { user }, chatStore: { selectedRoom } } = useStore();

    const [isAuthor, setIsAuthor] = useState(false);

    useEffect(() => {
        setIsAuthor(message.authorId === user?.id);
    }, []);

    return (
        <Box>
            <Tooltip hasArrow label={formatDistanceToNow(message.createdAt) + ' ago'}>
                <Box
                    p='2' m='1'
                    width='fit-content'
                    bgColor={isAuthor ? 'blue.500' : 'gray.500'}
                    float={isAuthor ? 'right' : 'left'}
                    borderRadius='1rem'
                    maxWidth='100%'
                >
                    <Text as='b' noOfLines={1}>
                        {selectedRoom?.users?.find(x => x.id === message.authorId)?.displayName}
                    </Text>
                    <Text>
                        {message.body}
                    </Text>
                </Box>
            </Tooltip>
        </Box>
    )
})