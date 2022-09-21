import { ArrowBackIcon, ChevronDownIcon, DeleteIcon, EditIcon, LinkIcon, SettingsIcon } from "@chakra-ui/icons";
import { Box, Button, Container, Flex, IconButton, Menu, MenuButton, MenuItem, MenuList, Popover, PopoverArrow, PopoverBody, PopoverCloseButton, PopoverContent, PopoverTrigger, Spacer, useDisclosure } from "@chakra-ui/react";
import { observer } from "mobx-react-lite";
import { toast } from "react-toastify";
import ConfirmModal from "../../../app/common/modals/ConfirmModal";
import InputModal from "../../../app/common/modals/InputModal";
import { store, useStore } from "../../../app/stores/store";



export default observer(function ChatRoomHeader() {
    const { chatStore, userStore, modalStore: { openModal } } = useStore();

    const handleInvitationClick = () => {
        if (!chatStore.selectedRoom) return;

        navigator.clipboard.writeText(chatStore.selectedRoom.id);
        toast.success('Copied invitation code to the clipboard');
    }

    const handleChangeDisplayNameClick = () => {
        openModal(<InputModal
            description='Enter your new name:'
            buttonText='Change'
            handleSubmit={(value) => chatStore.changeDisplayName(value)}
        />)
    }

    const handleDeleteUserClick = (userId: string, userName: string) => {
        openModal(<ConfirmModal 
            description={`Are you sure that you want to delete ${userName} from ${chatStore.selectedRoom?.name}?`}
            buttonText='Delete'
            handleSubmit={() => chatStore.deleteUserFromRoom(chatStore.selectedRoom!.id, userId)}
        />)
    }

    const handleDeleteRoomClick = () => {
        openModal(<ConfirmModal 
            description={`Are you sure that you want to delete ${chatStore.selectedRoom?.name}?`}
            buttonText='Delete'
            handleSubmit={() => chatStore.deleteRoom(chatStore.selectedRoom!.id)}
        />)
    }

    const handleExitRoomClick = () => {
        openModal(<ConfirmModal 
            description={`Are you sure that you want to exit ${chatStore.selectedRoom?.name}?`}
            buttonText='Exit'
            handleSubmit={() => chatStore.deleteUserFromRoom(chatStore.selectedRoom!.id, userStore.user!.id)}
        />)
    }

    return (
        <Container pos='absolute' top='0' p='0' maxWidth='100%'>
            <Flex>
                <Menu>
                    <MenuButton
                        as={Button}
                        rightIcon={<SettingsIcon />}
                        aria-label='Options'
                        borderRadius='1rem 0 1rem 0'
                        variant='main-style'
                    >
                        {store.chatStore.selectedRoom?.name || '{roomName}'}
                    </MenuButton>
                    <MenuList>
                        <MenuItem icon={<EditIcon />} onClick={handleChangeDisplayNameClick}>
                            Change display name
                        </MenuItem>
                        <MenuItem icon={<LinkIcon />} onClick={handleInvitationClick}>
                            Copy invitation code
                        </MenuItem>
                        {chatStore.isRoomAdministrator &&
                            <MenuItem icon={<DeleteIcon />} onClick={handleDeleteRoomClick}>
                                Delete room
                            </MenuItem>}
                        <MenuItem icon={<ArrowBackIcon />} onClick={handleExitRoomClick}>
                            Exit room
                        </MenuItem>
                    </MenuList>
                </Menu>
                <Spacer />
                <Popover placement='bottom-end'>
                    <PopoverTrigger>
                        <Button
                            leftIcon={<ChevronDownIcon />}
                            variant='main-style'
                            borderRadius='0 1rem 0 1rem'
                        >
                            Users ({chatStore.selectedRoom?.users?.length || '?'})
                        </Button>
                    </PopoverTrigger>
                    <PopoverContent pos='relative'>
                        <PopoverArrow />
                        <PopoverCloseButton pos='absolute' top='0' right='0' />
                        <PopoverBody mt='3'>
                            {chatStore.selectedRoom?.users?.map(user => (
                                <Box key={user.id} _hover={{ bgColor: 'gray.100' }} borderRadius='0.5rem' p='1'>
                                    {user.displayName}
                                    {user.id === userStore.user?.id && ' (You) '}
                                    {user.isAdministrator && ' (admin) '}
                                    {chatStore.isRoomAdministrator && !(user.id === userStore.user?.id) &&
                                        <IconButton
                                            aria-label='delete user'
                                            bgColor='transparent'
                                            size='xs' icon={<DeleteIcon />}
                                            onClick={() => handleDeleteUserClick(user.id, user.displayName)}
                                        />}
                                </Box>
                            ))}
                        </PopoverBody>
                    </PopoverContent>
                </Popover>
            </Flex>
        </Container>
    )
})