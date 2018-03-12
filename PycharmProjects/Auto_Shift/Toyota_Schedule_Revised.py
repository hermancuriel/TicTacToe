import requests
import csv
from bs4 import BeautifulSoup
from operator import add

event_links = []
event_info = []
event_date_time = []
artist_name = []

# Request event site from Toyota Center and make soup object
allevent_url = 'http://www.houstontoyotacenter.com/events/all'
html = requests.get(allevent_url)
page_text = html.text
allevent_soup = BeautifulSoup(page_text, 'lxml')
links = allevent_soup.findAll('a', {'class': 'btn more'})

# Parse links and add to list
for link in links:
    if link.has_attr('href'):
        event_links.append(link['href'])

# Scrape event data from each individual event link
for event_link in event_links:
    individualevent_url = event_link
    html1 = requests.get(individualevent_url)
    page_text1 = html1.text
    soup = BeautifulSoup(page_text1, 'lxml')
    event = soup.findAll('div', {'class': 'event_detail container clearfix'})
    event_info.append(event)

# Pull event name, time, date data and create lists
for event_list in event_info:
    for single_event in event_list:
        col = single_event.findAll('h1', {'class': 'summary show_mobile'})
        col1 = single_event.findAll('span', {'class': ['date', 'time']})
        artist_name.append([ele.text.strip() for ele in col])
        event_date_time.append([ele.text.strip() for ele in col1])

# Merge two lists in to one
event_data = map(add, artist_name, event_date_time)

# Export data to CSV file
with open('Toyota_Schedule.csv', 'w', newline='') as myfile:
    wr = csv.writer(myfile)
    wr.writerow(['subject', 'start date', 'start time', 'Location'])
    wr.writerows(event_data)
