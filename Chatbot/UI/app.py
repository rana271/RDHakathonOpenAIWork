import streamlit as st
import requests

st.title("Platform Support Chatbot")

st.markdown("""
    <style>
    .stButton>button {
        background-color: #4CAF50; /* Green background */
        color: white;
        border: none;
        padding: 8px 16px;
        border-radius: 5px;
        display: flex;
        align-items: center;
    }
    .stButton>button:hover {
        background-color: #45a049;
    }
    </style>
""", unsafe_allow_html=True)

# Input box for query
query = st.text_input("Ask a question (e.g., 'What’s the RCA for INC123?'):")

if query:
    # Call the FastAPI backend
    try:
        response = requests.get(f"http://localhost:8000/chat?query={query}")
        response.raise_for_status()
        result = response.json()["response"]
        st.write("**Answer:**", result)
    except Exception as e:
        st.write("Error:", str(e))