import { onBeforeUnmount, onMounted } from "vue";

/**
 * Hàm xử lý sự kiện click ra ngoài element
 * Author: LHH - 04/01/23
 */
export default function useDetectOutsideClick(component, callback) {
	if (!component) return;
	const listener = (event) => {
		if (
			event.target !== component.value &&
			event.composedPath().includes(component.value)
		) {
			return;
		}
		if (typeof callback === "function") {
			callback();
		}
	};
	onMounted(() => {
		window.addEventListener("click", listener);
	});
	onBeforeUnmount(() => {
		window.removeEventListener("click", listener);
	});

	return { listener };
}
